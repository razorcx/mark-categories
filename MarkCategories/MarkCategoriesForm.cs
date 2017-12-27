using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using Newtonsoft.Json;
using Tekla.Structures;
using Tekla.Structures.DrawingInternal;
using Tekla.Structures.Model;
using Tekla.Structures.Model.UI;

namespace MarkCategories
{
	public partial class MarkCategoriesForm : Form
	{
		private readonly Model _model = new Model();
		private readonly string _folder;

		public MarkCategoriesForm()
		{
			InitializeComponent();

			_folder = _model.GetInfo().ModelPath + @"\RazorCX\Data";
		}

		private void MarkCategoriesForm_Load(object sender, EventArgs e)
		{
			var path = _folder + @"\MarkCategories_Standard.json";

			LoadFormData(path);

			PopulateComboBoxLoad();
		}

		private void LoadFormData(string path)
		{
			try
			{
				var file = string.Empty;
				using (var reader = new StreamReader(path))
				{
					file = reader.ReadToEnd();
				}

				var markCategories = file.FromJson<List<MarkCategory>>();

				var markCategoryViews = markCategories
					.Select(m => new MarkCategoryView(m))
					.ToList();

				dataGridViewMarkCategories.DataSource = markCategoryViews;

				foreach (DataGridViewRow row in dataGridViewMarkCategories.Rows)
				{
					row.Tag = markCategories
						.FirstOrDefault(m => m.Id == ((MarkCategoryView) row.DataBoundItem).CatId);
				}

				var mainParts = GetMainParts();

				var names = mainParts.Select(p => p.Name).Distinct().ToList();

				var nameMarkCategories = names.Select(p =>
						new NameMarkCategory()
						{
							Name = p,
							CatId = 0,
						})
					.OrderBy(p => p.Name)
					.ToList();

				var count = 1;
				nameMarkCategories.ForEach(p => p.Id = count++);

				nameMarkCategories.ForEach(m =>
				{
					var firstOrDefault = markCategories.FirstOrDefault(c => c.Name == m.Name);
					if (firstOrDefault != null)
						m.CatId = firstOrDefault.Id;
				});

				dataGridViewNames.DataSource = nameMarkCategories;

				var nameMarkCategory = dataGridViewNames.CurrentRow?.DataBoundItem as NameMarkCategory;
				if (nameMarkCategory == null) return;

				UpdatePartsView(nameMarkCategory.Name);

			}
			catch (Exception ex)
			{
				
			}
		}

		private void PopulateComboBoxLoad()
		{
			if (Directory.Exists(_folder))
			{
				var files = Directory.GetFiles(_folder)
					.Select(f => f
						.Split('\\').Last()
						.Split('_').Last()
						.Split('.').First())
						.ToList();
				comboBoxLoad.DataSource = files;
			}
		}


		private void buttonSave_Click(object sender, EventArgs e)
		{
			if (string.IsNullOrWhiteSpace(textBoxSave.Text)) return;

			var markCategories = dataGridViewMarkCategories.Rows
				.OfType<DataGridViewRow>()
				.ToList()
				.Select(r => r.Tag as MarkCategory)
				.ToList();

			var json = markCategories.ToJson();

			var folder = new Model().GetInfo().ModelPath + @"\RazorCX\Data";

			if (!Directory.Exists(folder))
			{
				Directory.CreateDirectory(folder);
			}

			var path = folder + $"\\MarkCategories_{textBoxSave.Text}.json";

			using (var writer = new StreamWriter(path))
			{
				writer.Write(json);
			}
		}

		private void buttonLoad_Click(object sender, EventArgs e)
		{
			var path = _folder + $"\\MarkCategories_{comboBoxLoad.SelectedItem}.json";
			LoadFormData(path);
		}

		private void comboBoxLoad_Click(object sender, EventArgs e)
		{
			PopulateComboBoxLoad();
		}


		private void buttonAddMarkCategory_Click(object sender, EventArgs e)
		{
			var mc = new MarkCategory
			{
				Id = dataGridViewMarkCategories.RowCount + 1,
				Name = "",
				PartNumber = new NumberingSeries("x", 1),
				AssemblyNumber = new NumberingSeries("X", 1000),
				HasPhase = false,
				HasDash = false,
				Parts = new List<RazorPart>()
			};

			var markCategories = dataGridViewMarkCategories.Rows
				.OfType<DataGridViewRow>()
				.ToList()
				.Select(r => r.Tag as MarkCategory)
				.ToList();

			markCategories.Add(mc);

			UpdateDataGridView(markCategories);
		}

		private void buttonDeleteCategory_Click(object sender, EventArgs e)
		{
			var markCategory = dataGridViewMarkCategories.CurrentRow?.Tag as MarkCategory;

			var markCategories = dataGridViewMarkCategories.Rows
				.OfType<DataGridViewRow>()
				.ToList()
				.Select(r => r.Tag as MarkCategory)
				.ToList();

			markCategories.Remove(markCategory);

			var id = 1;
			markCategories.ForEach(m => m.Id = id++);

			UpdateDataGridView(markCategories);
		}

		private void buttonSelect_Click(object sender, EventArgs e)
		{
			var markCategoryView = dataGridViewMarkCategories.CurrentRow?.DataBoundItem as MarkCategoryView;
			var markCategory = dataGridViewMarkCategories.CurrentRow?.Tag as MarkCategory;

			markCategory.AssemblyNumber.Prefix = markCategoryView.A_Prefix;
			markCategory.AssemblyNumber.StartNumber = markCategoryView.A_Number;
			markCategory.PartNumber.Prefix = markCategoryView.P_Prefix;
			markCategory.PartNumber.StartNumber = markCategoryView.P_Number;

			try
			{
				var picker = new Picker();
				var parts = picker.PickObjects(Picker.PickObjectsEnum.PICK_N_PARTS).ToAList<Part>();

				ApplyMarks(parts, markCategory, markCategoryView);
			}
			catch (Exception ex)
			{
				
			}
		}

		private void dataGridViewMarkCategories_CellEndEdit(object sender, DataGridViewCellEventArgs e)
		{
			var row = dataGridViewMarkCategories.CurrentRow?.DataBoundItem as MarkCategoryView;
			var markCategory = dataGridViewMarkCategories.CurrentRow?.Tag as MarkCategory;

			markCategory.Name = row.Name;
			markCategory.HasPhase = row.HasPhase;
			markCategory.HasDash = row.HasDash;

			markCategory.AssemblyNumber = new NumberingSeries
			{
				Prefix = row.A_Prefix,
				StartNumber = row.A_Number
			};

			markCategory.PartNumber = new NumberingSeries
			{
				Prefix = row.P_Prefix,
				StartNumber = row.P_Number
			};
		}


		private void buttonSelectPartsInModel_Click(object sender, EventArgs e)
		{
			var row = dataGridViewNames.CurrentRow;
			if(row == null) return;

			var name = ((NameMarkCategory) row.DataBoundItem).Name;

			var modelObjects = new ArrayList();

			var mainParts = GetMainParts();

			mainParts.ForEach(p =>
			{
				if (p.Name == name) modelObjects.Add(p);
			});

			new Tekla.Structures.Model.UI.ModelObjectSelector().Select(modelObjects);
		}

		private void buttonAddCategory_Click(object sender, EventArgs e)
		{
			var row = dataGridViewNames.CurrentRow;
			if (row == null) return;

			var nameMarkCategory = (NameMarkCategory) row.DataBoundItem;

			var markCategories = dataGridViewMarkCategories.Rows
				.OfType<DataGridViewRow>()
				.ToList()
				.Select(r => r.Tag as MarkCategory)
				.ToList();

			var names = markCategories.Select(m => m.Name).ToList();

			if (names.Contains(nameMarkCategory.Name)) return;

			var id = dataGridViewMarkCategories.RowCount + 1;
			nameMarkCategory.CatId = id;

			var mc = new MarkCategory
			{
				Id = id,
				Name = nameMarkCategory.Name,
				PartNumber = new NumberingSeries("x", 1),
				AssemblyNumber = new NumberingSeries("X", 1000),
				HasPhase = false,
				HasDash = false,
				Parts = new List<RazorPart>()
			};

			markCategories.Add(mc);

			UpdateDataGridView(markCategories);
		}

		private void buttonAddAllCategory_Click(object sender, EventArgs e)
		{
			var rows = dataGridViewNames.Rows;

			var markCategories = dataGridViewMarkCategories.Rows
				.OfType<DataGridViewRow>()
				.ToList()
				.Select(r => r.Tag as MarkCategory)
				.ToList();

			var names = markCategories.Select(m => m.Name).ToList();

			var id = dataGridViewMarkCategories.RowCount;

			foreach (DataGridViewRow row in rows)
			{
				var nameMarkCategory = (NameMarkCategory)row.DataBoundItem;

				if(names.Contains(nameMarkCategory.Name)) continue;

				id++;

				nameMarkCategory.CatId = id;

				var mc = new MarkCategory
				{
					Id = id,
					Name = nameMarkCategory.Name,
					PartNumber = new NumberingSeries("x", 1),
					AssemblyNumber = new NumberingSeries("X", 1000),
					HasPhase = false,
					HasDash = false,
					Parts = new List<RazorPart>()
				};

				markCategories.Add(mc);
			}

			UpdateDataGridView(markCategories);
		}

		private void buttonApplyMarks_Click(object sender, EventArgs e)
		{
			var row = dataGridViewNames.CurrentRow;
			if (row == null) return;
			
			ApplyMarks(row);
		}

		private void buttonApplyAllMarks_Click(object sender, EventArgs e)
		{
			var rows = dataGridViewNames.Rows.OfType<DataGridViewRow>().ToList();

			rows
				.Where(r => ((NameMarkCategory)r.DataBoundItem).CatId > 0)
				.ToList()
				.ForEach(ApplyMarks);
		}

		private void dataGridViewParts_Click(object sender, EventArgs e)
		{
			var razorPartView = dataGridViewParts.CurrentRow?.DataBoundItem as RazorPartView;
			if (razorPartView == null) return;

			var modelObject = _model.SelectModelObject(new Identifier(razorPartView.Id));
			var modelObjects = new ArrayList { modelObject };

			new Tekla.Structures.Model.UI.ModelObjectSelector().Select(modelObjects);
		}


		private void UpdateDataGridView(List<MarkCategory> markCategories)
		{
			if (markCategories == null)
				markCategories = dataGridViewMarkCategories.Rows
					.OfType<DataGridViewRow>()
					.ToList()
					.Select(r => r.Tag as MarkCategory)
					.ToList();

			dataGridViewMarkCategories.DataSource = null;

			var markCategoryViews = markCategories
				.Select(m => new MarkCategoryView(m))
				.ToList();

			dataGridViewMarkCategories.DataSource = markCategoryViews;

			foreach (DataGridViewRow rr in dataGridViewMarkCategories.Rows)
			{
				rr.Tag = markCategories.FirstOrDefault(m => m.Id == ((MarkCategoryView)rr.DataBoundItem).CatId);
			}
		}

		private void ApplyMarks(DataGridViewRow row)
		{
			var nameMarkCategory = (NameMarkCategory)row.DataBoundItem;
			var catId = nameMarkCategory.CatId;

			var markCategoryView = dataGridViewMarkCategories.Rows
				.OfType<DataGridViewRow>()
				.Select(r => r.DataBoundItem as MarkCategoryView)
				.FirstOrDefault(r => r.CatId == catId);

			var markCategory = dataGridViewMarkCategories.Rows
				.OfType<DataGridViewRow>()
				.Select(r => r.Tag as MarkCategory)
				.FirstOrDefault(r => r.Id == catId);

			var mainParts = GetMainParts()
				.Where(p => p.Name == nameMarkCategory.Name)
				.ToList();

			ApplyMarks(mainParts, markCategory, markCategoryView);

			UpdatePartsView(nameMarkCategory.Name);
		}

		private void ApplyMarks(List<Part> parts, MarkCategory markCategory, MarkCategoryView markCategoryView)
		{
			if (markCategory == null || markCategoryView == null) return;

			parts.ForEach(p =>
			{
				p.GetPhase(out Phase phase);

				var prefix = markCategory.AssemblyNumber.Prefix;

				if (markCategoryView.HasPhase && !markCategoryView.HasDash)
				{
					prefix = $"{phase.PhaseNumber}{markCategory.AssemblyNumber.Prefix}";
				}

				if (!markCategoryView.HasPhase && markCategoryView.HasDash)
				{
					prefix = $"{markCategory.AssemblyNumber.Prefix}-";
				}

				if (markCategoryView.HasPhase && markCategoryView.HasDash)
				{
					prefix = $"{phase.PhaseNumber}{markCategory.AssemblyNumber.Prefix}-";
				}

				p.AssemblyNumber.Prefix = prefix;
				p.AssemblyNumber.StartNumber = markCategory.AssemblyNumber.StartNumber;

				p.PartNumber = markCategory.PartNumber;
				p.Modify();
			});

			var razorParts = parts.ToJson().FromJson<List<RazorPart>>();
			markCategory.Parts = razorParts;

			UpdateDataGridView(null);
		}

		private List<Part> GetMainParts()
		{
			ModelObjectEnumerator.AutoFetch = true;

			var beams = _model.GetModelObjectSelector()
				.GetAllObjectsWithType(ModelObject.ModelObjectEnum.BEAM)
				.ToAList<Beam>();

			var bentPlates = _model.GetModelObjectSelector()
				.GetAllObjectsWithType(ModelObject.ModelObjectEnum.BENT_PLATE)
				.ToAList<BentPlate>();

			var contourPlates = _model.GetModelObjectSelector()
				.GetAllObjectsWithType(ModelObject.ModelObjectEnum.CONTOURPLATE)
				.ToAList<ContourPlate>();

			var polybeams = _model.GetModelObjectSelector()
				.GetAllObjectsWithType(ModelObject.ModelObjectEnum.POLYBEAM)
				.ToAList<PolyBeam>();

			var parts = new List<Part>();

			parts.AddRange(beams);
			parts.AddRange(bentPlates);
			parts.AddRange(contourPlates);
			parts.AddRange(polybeams);

			var mainParts = parts.AsParallel().Where(p =>
			{
				int isMainPart = 0;
				p.GetReportProperty("MAIN_PART", ref isMainPart);
				return isMainPart > 0;
			}).ToList();

			return mainParts;
		}

		private void dataGridViewNames_Click(object sender, EventArgs e)
		{
			var nameMarkCategory = dataGridViewNames.CurrentRow?.DataBoundItem as NameMarkCategory;
			if (nameMarkCategory == null) return;

			UpdatePartsView(nameMarkCategory.Name);
		}

		private void UpdatePartsView(string name)
		{
			var mainParts = GetMainParts()
				.Where(p => p.Name == name)
				.ToList();

			var razorPartViews = mainParts.Select(p =>
				{
					p.GetPhase(out Phase phase);
					return new RazorPartView
					{
						PartMark = p.GetPartMark(),
						Phase = phase.PhaseNumber,
						Name = p.Name,
						Profile = p.Profile.ProfileString,
						Material = p.Material.MaterialString,
						Class = p.Class,
						Finish = p.Finish,
						A_Prefix = p.AssemblyNumber.Prefix,
						A_Number = p.AssemblyNumber.StartNumber,
						P_Prefix = p.PartNumber.Prefix,
						P_Number = p.PartNumber.StartNumber,
						Id = p.Identifier.ID,
					};
				})
				.OrderBy(p => p.Phase)
				.ThenBy(p => p.PartMark)
				.ToList();

			dataGridViewParts.DataSource = razorPartViews;
		}
	}
}
