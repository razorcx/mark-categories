using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using Tekla.Structures.Model;
using Tekla.Structures.Model.UI;

namespace MarkCategories
{
	public partial class MarkCategoriesForm : Form
	{
		public MarkCategoriesForm()
		{
			InitializeComponent();
		}

		private void MarkCategoriesForm_Load(object sender, EventArgs e)
		{
			var folder = new Model().GetInfo().ModelPath + @"\RazorCX\Data";
			var path = folder + @"\MarkCategories.json";

			var file = string.Empty;
			using (var reader = new StreamReader(path))
			{
				file = reader.ReadToEnd();
			}

			var markCategories = file.FromJson<List<MarkCategory>>();

			var markCategoryViews = markCategories.Select(m => new MarkCategoryView 
			{
				Id = m.Id,
				Description = m.Description,
				A_Prefix = m.AssemblyNumber.Prefix,
				A_Number = m.AssemblyNumber.StartNumber,
				P_Prefix = m.PartNumber.Prefix,
				P_Number = m.PartNumber.StartNumber,
				HasPhase = m.HasPhase,
				HasDash = m.HasDash,
				Quantity = m.Parts.Count,
			}).ToList();

			dataGridViewMarkCategories.DataSource = markCategoryViews;

			foreach (DataGridViewRow row in dataGridViewMarkCategories.Rows)
			{
				row.Tag = markCategories.FirstOrDefault(m => m.Id == ((MarkCategoryView) row.DataBoundItem).Id);
			}
		}

		private void buttonSelect_Click(object sender, EventArgs e)
		{
			var row = dataGridViewMarkCategories.CurrentRow?.DataBoundItem as MarkCategoryView;
			var markCategory = dataGridViewMarkCategories.CurrentRow?.Tag as MarkCategory;

			markCategory.AssemblyNumber.Prefix = row.A_Prefix;
			markCategory.AssemblyNumber.StartNumber = row.A_Number;
			markCategory.PartNumber.Prefix = row.P_Prefix;
			markCategory.PartNumber.StartNumber = row.P_Number;

			try
			{
				var picker = new Picker();
				var parts = picker.PickObjects(Picker.PickObjectsEnum.PICK_N_PARTS).ToAList<Part>();

				parts.ForEach(p =>
				{
					var prefix = string.Empty;

					p.GetPhase(out Phase phase);

					prefix = markCategory.AssemblyNumber.Prefix;

					if (row.HasPhase && !row.HasDash)
					{
						prefix = $"{phase.PhaseNumber}{markCategory.AssemblyNumber.Prefix}";
					}

					if (!row.HasPhase && row.HasDash)
					{
						prefix = $"{markCategory.AssemblyNumber.Prefix}-";
					}

					if (row.HasPhase && row.HasDash)
					{
						prefix = $"{phase.PhaseNumber}{markCategory.AssemblyNumber.Prefix}-";
					}

					p.AssemblyNumber.Prefix = prefix;
					p.AssemblyNumber.StartNumber = markCategory.AssemblyNumber.StartNumber;

					p.PartNumber = markCategory.PartNumber;
					p.Modify();
				});

				markCategory.Parts = new List<Part>(parts);

				UpdateDataGridView(null);
			}
			catch (Exception ex)
			{
				
			}
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
				Parts = new List<Part>()
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

			var path = folder + @"\MarkCategories.json";

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

			var markCategoryViews = markCategories.Select(m => new MarkCategoryView
			{
				Id = m.Id,
				Description = m.Description,
				A_Prefix = m.AssemblyNumber.Prefix,
				A_Number = m.AssemblyNumber.StartNumber,
				P_Prefix = m.PartNumber.Prefix,
				P_Number = m.PartNumber.StartNumber,
				HasPhase = m.HasPhase,
				HasDash = m.HasDash,
				Quantity = m.Parts.Count,
			}).ToList();

			dataGridViewMarkCategories.DataSource = markCategoryViews;

			foreach (DataGridViewRow rr in dataGridViewMarkCategories.Rows)
			{
				rr.Tag = markCategories.FirstOrDefault(m => m.Id == ((MarkCategoryView)rr.DataBoundItem).Id);
			}
		}

		private void buttonLoad_Click(object sender, EventArgs e)
		{

		}
	}
}
