using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using Newtonsoft.Json;
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

				ModelObjectEnumerator.AutoFetch = true;
				var parts = _model.GetModelObjectSelector()
					.GetAllObjects().ToAList<Part>();

				var mainParts = parts.AsParallel().Where(p =>
				{
					int isMainPart = 0;
					p.GetReportProperty("MAIN_PART", ref isMainPart);
					return isMainPart > 0;
				}).ToList();

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
					var firstOrDefault = markCategories.FirstOrDefault(c => c.Description == m.Name);
					if (firstOrDefault != null)
						m.CatId = firstOrDefault.Id;
				});

				dataGridViewNames.DataSource = nameMarkCategories;
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

		private void ApplyMarks(List<Part> parts, MarkCategory markCategory, MarkCategoryView markCategoryView)
		{
			var mainParts = parts.Where(p =>
			{
				int isMainPart = 0;
				p.GetReportProperty("MAIN_PART", ref isMainPart);
				return isMainPart > 0;
			}).ToList();

			mainParts.ForEach(p =>
			{
				var prefix = string.Empty;

				p.GetPhase(out Phase phase);

				prefix = markCategory.AssemblyNumber.Prefix;

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

		private void buttonAddMarkCategory_Click(object sender, EventArgs e)
		{
			var mc = new MarkCategory
			{
				Id = dataGridViewMarkCategories.RowCount + 1,
				Description = "",
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

		private void dataGridViewMarkCategories_CellEndEdit(object sender, DataGridViewCellEventArgs e)
		{
			var row = dataGridViewMarkCategories.CurrentRow?.DataBoundItem as MarkCategoryView;
			var markCategory = dataGridViewMarkCategories.CurrentRow?.Tag as MarkCategory;

			markCategory.Description = row.Description;
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

			if(!Directory.Exists(folder))
			{
				Directory.CreateDirectory(folder);
			}

			var path = folder + $"\\MarkCategories_{textBoxSave.Text}.json";

			using (var writer = new StreamWriter(path))
			{
				writer.Write(json);
			}
		}

		private void UpdateDataGridView(List<MarkCategory> markCategories)
		{
			if(markCategories == null || !markCategories.Any())
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

		private void buttonLoad_Click(object sender, EventArgs e)
		{
			var path = _folder + $"\\MarkCategories_{comboBoxLoad.SelectedItem}.json";
			LoadFormData(path);
		}

		private void comboBoxLoad_Click(object sender, EventArgs e)
		{
			PopulateComboBoxLoad();
		}

		private void buttonSelectPartsInModel_Click(object sender, EventArgs e)
		{
			var row = dataGridViewNames.CurrentRow;
			if(row == null) return;

			var name = ((NameMarkCategory) row.DataBoundItem).Name;

			var modelObjects = new ArrayList();

			ModelObjectEnumerator.AutoFetch = true;
			var parts = _model.GetModelObjectSelector()
				.GetAllObjects().ToAList<Part>();

			var mainParts = parts.AsParallel().Where(p =>
			{
				int isMainPart = 0;
				p.GetReportProperty("MAIN_PART", ref isMainPart);
				return isMainPart > 0;
			}).ToList();

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

			var id = dataGridViewMarkCategories.RowCount + 1;
			nameMarkCategory.CatId = id;

			var mc = new MarkCategory
			{
				Id = id,
				Description = nameMarkCategory.Name,
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

		private void buttonAddAllCategory_Click(object sender, EventArgs e)
		{
			var rows = dataGridViewNames.Rows;

			var markCategories = dataGridViewMarkCategories.Rows
				.OfType<DataGridViewRow>()
				.ToList()
				.Select(r => r.Tag as MarkCategory)
				.ToList();

			var names = markCategories.Select(m => m.Description).ToList();

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
					Description = nameMarkCategory.Name,
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

		private void buttonDeleteCategory_Click(object sender, EventArgs e)
		{
			var markCategory = dataGridViewMarkCategories.CurrentRow?.Tag as MarkCategory;

			var markCategories = dataGridViewMarkCategories.Rows
				.OfType<DataGridViewRow>()
				.ToList()
				.Select(r => r.Tag as MarkCategory)
				.ToList();

			markCategories.Remove(markCategory);

			UpdateDataGridView(markCategories);

		}

		private void buttonApplyMarks_Click(object sender, EventArgs e)
		{
			var row = dataGridViewNames.CurrentRow;
			if (row == null) return;

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

			ModelObjectEnumerator.AutoFetch = true;
			var parts = _model.GetModelObjectSelector()
				.GetAllObjects()
				.ToAList<Part>()
				.Where(p => p.Name == nameMarkCategory.Name)
				.ToList();

			ApplyMarks(parts, markCategory, markCategoryView);
			
		}

		private void buttonApplyAllMarks_Click(object sender, EventArgs e)
		{
			var rows = dataGridViewNames.Rows.OfType<DataGridViewRow>().ToList();

			rows
				.Where(r => ((NameMarkCategory)r.DataBoundItem).CatId > 0)
				.ToList()
				.ForEach(row =>
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

					ModelObjectEnumerator.AutoFetch = true;
					var parts = _model.GetModelObjectSelector()
						.GetAllObjects()
						.ToAList<Part>()
						.Where(p => p.Name == nameMarkCategory.Name)
						.ToList();

					ApplyMarks(parts, markCategory, markCategoryView);
				});
		}
	}
}
