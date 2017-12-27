namespace MarkCategories
{
	partial class MarkCategoriesForm
	{
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing && (components != null))
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Windows Form Designer generated code

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MarkCategoriesForm));
			this.pictureBox1 = new System.Windows.Forms.PictureBox();
			this.dataGridViewMarkCategories = new System.Windows.Forms.DataGridView();
			this.buttonAddMarkCategory = new System.Windows.Forms.Button();
			this.buttonSelect = new System.Windows.Forms.Button();
			this.buttonSave = new System.Windows.Forms.Button();
			this.buttonLoad = new System.Windows.Forms.Button();
			this.dataGridViewNames = new System.Windows.Forms.DataGridView();
			this.textBoxSave = new System.Windows.Forms.TextBox();
			this.comboBoxLoad = new System.Windows.Forms.ComboBox();
			this.labelMarkCategories = new System.Windows.Forms.Label();
			this.label1 = new System.Windows.Forms.Label();
			this.buttonDeleteCategory = new System.Windows.Forms.Button();
			this.buttonSelectPartsInModel = new System.Windows.Forms.Button();
			this.buttonAddAllCategory = new System.Windows.Forms.Button();
			this.button2 = new System.Windows.Forms.Button();
			this.buttonApplyMarks = new System.Windows.Forms.Button();
			this.buttonApplyAllMarks = new System.Windows.Forms.Button();
			this.dataGridViewParts = new System.Windows.Forms.DataGridView();
			this.labelParts = new System.Windows.Forms.Label();
			((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.dataGridViewMarkCategories)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.dataGridViewNames)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.dataGridViewParts)).BeginInit();
			this.SuspendLayout();
			// 
			// pictureBox1
			// 
			this.pictureBox1.Image = global::MarkCategories.Properties.Resources.Logo;
			this.pictureBox1.Location = new System.Drawing.Point(12, 12);
			this.pictureBox1.Name = "pictureBox1";
			this.pictureBox1.Size = new System.Drawing.Size(160, 41);
			this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
			this.pictureBox1.TabIndex = 0;
			this.pictureBox1.TabStop = false;
			// 
			// dataGridViewMarkCategories
			// 
			this.dataGridViewMarkCategories.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.DisplayedCells;
			this.dataGridViewMarkCategories.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			this.dataGridViewMarkCategories.Location = new System.Drawing.Point(12, 98);
			this.dataGridViewMarkCategories.Name = "dataGridViewMarkCategories";
			this.dataGridViewMarkCategories.RowTemplate.Height = 24;
			this.dataGridViewMarkCategories.Size = new System.Drawing.Size(990, 230);
			this.dataGridViewMarkCategories.TabIndex = 1;
			this.dataGridViewMarkCategories.CellEndEdit += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridViewMarkCategories_CellEndEdit);
			// 
			// buttonAddMarkCategory
			// 
			this.buttonAddMarkCategory.Location = new System.Drawing.Point(570, 59);
			this.buttonAddMarkCategory.Name = "buttonAddMarkCategory";
			this.buttonAddMarkCategory.Size = new System.Drawing.Size(140, 33);
			this.buttonAddMarkCategory.TabIndex = 2;
			this.buttonAddMarkCategory.Text = "Add Category";
			this.buttonAddMarkCategory.UseVisualStyleBackColor = true;
			this.buttonAddMarkCategory.Click += new System.EventHandler(this.buttonAddMarkCategory_Click);
			// 
			// buttonSelect
			// 
			this.buttonSelect.Location = new System.Drawing.Point(862, 59);
			this.buttonSelect.Name = "buttonSelect";
			this.buttonSelect.Size = new System.Drawing.Size(140, 33);
			this.buttonSelect.TabIndex = 4;
			this.buttonSelect.Text = "Select Parts";
			this.buttonSelect.UseVisualStyleBackColor = true;
			this.buttonSelect.Click += new System.EventHandler(this.buttonSelect_Click);
			// 
			// buttonSave
			// 
			this.buttonSave.Location = new System.Drawing.Point(680, 13);
			this.buttonSave.Name = "buttonSave";
			this.buttonSave.Size = new System.Drawing.Size(90, 28);
			this.buttonSave.TabIndex = 5;
			this.buttonSave.Text = "Save";
			this.buttonSave.UseVisualStyleBackColor = true;
			this.buttonSave.Click += new System.EventHandler(this.buttonSave_Click);
			// 
			// buttonLoad
			// 
			this.buttonLoad.Location = new System.Drawing.Point(352, 13);
			this.buttonLoad.Name = "buttonLoad";
			this.buttonLoad.Size = new System.Drawing.Size(90, 28);
			this.buttonLoad.TabIndex = 6;
			this.buttonLoad.Text = "Load";
			this.buttonLoad.UseVisualStyleBackColor = true;
			this.buttonLoad.Click += new System.EventHandler(this.buttonLoad_Click);
			// 
			// dataGridViewNames
			// 
			this.dataGridViewNames.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.DisplayedCells;
			this.dataGridViewNames.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			this.dataGridViewNames.Location = new System.Drawing.Point(12, 366);
			this.dataGridViewNames.Name = "dataGridViewNames";
			this.dataGridViewNames.RowTemplate.Height = 24;
			this.dataGridViewNames.Size = new System.Drawing.Size(319, 235);
			this.dataGridViewNames.TabIndex = 7;
			this.dataGridViewNames.Click += new System.EventHandler(this.dataGridViewNames_Click);
			// 
			// textBoxSave
			// 
			this.textBoxSave.Location = new System.Drawing.Point(776, 15);
			this.textBoxSave.Name = "textBoxSave";
			this.textBoxSave.Size = new System.Drawing.Size(226, 22);
			this.textBoxSave.TabIndex = 8;
			// 
			// comboBoxLoad
			// 
			this.comboBoxLoad.FormattingEnabled = true;
			this.comboBoxLoad.Location = new System.Drawing.Point(448, 15);
			this.comboBoxLoad.Name = "comboBoxLoad";
			this.comboBoxLoad.Size = new System.Drawing.Size(226, 24);
			this.comboBoxLoad.TabIndex = 9;
			this.comboBoxLoad.Click += new System.EventHandler(this.comboBoxLoad_Click);
			// 
			// labelMarkCategories
			// 
			this.labelMarkCategories.AutoSize = true;
			this.labelMarkCategories.Location = new System.Drawing.Point(12, 75);
			this.labelMarkCategories.Name = "labelMarkCategories";
			this.labelMarkCategories.Size = new System.Drawing.Size(111, 17);
			this.labelMarkCategories.TabIndex = 10;
			this.labelMarkCategories.Text = "Mark Categories";
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(12, 346);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(136, 17);
			this.label1.TabIndex = 11;
			this.label1.Text = "Categories by Name";
			// 
			// buttonDeleteCategory
			// 
			this.buttonDeleteCategory.Location = new System.Drawing.Point(716, 59);
			this.buttonDeleteCategory.Name = "buttonDeleteCategory";
			this.buttonDeleteCategory.Size = new System.Drawing.Size(140, 33);
			this.buttonDeleteCategory.TabIndex = 12;
			this.buttonDeleteCategory.Text = "Delete Category";
			this.buttonDeleteCategory.UseVisualStyleBackColor = true;
			this.buttonDeleteCategory.Click += new System.EventHandler(this.buttonDeleteCategory_Click);
			// 
			// buttonSelectPartsInModel
			// 
			this.buttonSelectPartsInModel.Location = new System.Drawing.Point(337, 466);
			this.buttonSelectPartsInModel.Name = "buttonSelectPartsInModel";
			this.buttonSelectPartsInModel.Size = new System.Drawing.Size(140, 33);
			this.buttonSelectPartsInModel.TabIndex = 13;
			this.buttonSelectPartsInModel.Text = "Select In Model";
			this.buttonSelectPartsInModel.UseVisualStyleBackColor = true;
			this.buttonSelectPartsInModel.Click += new System.EventHandler(this.buttonSelectPartsInModel_Click);
			// 
			// buttonAddAllCategory
			// 
			this.buttonAddAllCategory.Location = new System.Drawing.Point(337, 405);
			this.buttonAddAllCategory.Name = "buttonAddAllCategory";
			this.buttonAddAllCategory.Size = new System.Drawing.Size(140, 33);
			this.buttonAddAllCategory.TabIndex = 14;
			this.buttonAddAllCategory.Text = "Add All Categories";
			this.buttonAddAllCategory.UseVisualStyleBackColor = true;
			this.buttonAddAllCategory.Click += new System.EventHandler(this.buttonAddAllCategory_Click);
			// 
			// button2
			// 
			this.button2.Location = new System.Drawing.Point(337, 366);
			this.button2.Name = "button2";
			this.button2.Size = new System.Drawing.Size(140, 33);
			this.button2.TabIndex = 15;
			this.button2.Text = "Add Category";
			this.button2.UseVisualStyleBackColor = true;
			this.button2.Click += new System.EventHandler(this.buttonAddCategory_Click);
			// 
			// buttonApplyMarks
			// 
			this.buttonApplyMarks.Location = new System.Drawing.Point(337, 529);
			this.buttonApplyMarks.Name = "buttonApplyMarks";
			this.buttonApplyMarks.Size = new System.Drawing.Size(140, 33);
			this.buttonApplyMarks.TabIndex = 16;
			this.buttonApplyMarks.Text = "Apply Marks";
			this.buttonApplyMarks.UseVisualStyleBackColor = true;
			this.buttonApplyMarks.Click += new System.EventHandler(this.buttonApplyMarks_Click);
			// 
			// buttonApplyAllMarks
			// 
			this.buttonApplyAllMarks.Location = new System.Drawing.Point(337, 568);
			this.buttonApplyAllMarks.Name = "buttonApplyAllMarks";
			this.buttonApplyAllMarks.Size = new System.Drawing.Size(140, 33);
			this.buttonApplyAllMarks.TabIndex = 17;
			this.buttonApplyAllMarks.Text = "Apply All Marks";
			this.buttonApplyAllMarks.UseVisualStyleBackColor = true;
			this.buttonApplyAllMarks.Click += new System.EventHandler(this.buttonApplyAllMarks_Click);
			// 
			// dataGridViewParts
			// 
			this.dataGridViewParts.AllowUserToAddRows = false;
			this.dataGridViewParts.AllowUserToDeleteRows = false;
			this.dataGridViewParts.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.DisplayedCells;
			this.dataGridViewParts.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			this.dataGridViewParts.Location = new System.Drawing.Point(484, 366);
			this.dataGridViewParts.Name = "dataGridViewParts";
			this.dataGridViewParts.ReadOnly = true;
			this.dataGridViewParts.RowTemplate.Height = 24;
			this.dataGridViewParts.Size = new System.Drawing.Size(518, 235);
			this.dataGridViewParts.TabIndex = 18;
			this.dataGridViewParts.Click += new System.EventHandler(this.dataGridViewParts_Click);
			// 
			// labelParts
			// 
			this.labelParts.AutoSize = true;
			this.labelParts.Location = new System.Drawing.Point(483, 346);
			this.labelParts.Name = "labelParts";
			this.labelParts.Size = new System.Drawing.Size(127, 17);
			this.labelParts.TabIndex = 19;
			this.labelParts.Text = "Parts List by Name";
			// 
			// MarkCategoriesForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(1014, 613);
			this.Controls.Add(this.labelParts);
			this.Controls.Add(this.dataGridViewParts);
			this.Controls.Add(this.buttonApplyAllMarks);
			this.Controls.Add(this.buttonApplyMarks);
			this.Controls.Add(this.button2);
			this.Controls.Add(this.buttonAddAllCategory);
			this.Controls.Add(this.buttonSelectPartsInModel);
			this.Controls.Add(this.buttonDeleteCategory);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.labelMarkCategories);
			this.Controls.Add(this.comboBoxLoad);
			this.Controls.Add(this.textBoxSave);
			this.Controls.Add(this.dataGridViewNames);
			this.Controls.Add(this.buttonLoad);
			this.Controls.Add(this.buttonSave);
			this.Controls.Add(this.buttonSelect);
			this.Controls.Add(this.buttonAddMarkCategory);
			this.Controls.Add(this.dataGridViewMarkCategories);
			this.Controls.Add(this.pictureBox1);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MaximumSize = new System.Drawing.Size(1032, 659);
			this.Name = "MarkCategoriesForm";
			this.Text = "Mark Categories";
			this.Load += new System.EventHandler(this.MarkCategoriesForm_Load);
			((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.dataGridViewMarkCategories)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.dataGridViewNames)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.dataGridViewParts)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.PictureBox pictureBox1;
		private System.Windows.Forms.DataGridView dataGridViewMarkCategories;
		private System.Windows.Forms.Button buttonAddMarkCategory;
		private System.Windows.Forms.Button buttonSelect;
		private System.Windows.Forms.Button buttonSave;
		private System.Windows.Forms.Button buttonLoad;
		private System.Windows.Forms.DataGridView dataGridViewNames;
		private System.Windows.Forms.TextBox textBoxSave;
		private System.Windows.Forms.ComboBox comboBoxLoad;
		private System.Windows.Forms.Label labelMarkCategories;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Button buttonDeleteCategory;
		private System.Windows.Forms.Button buttonSelectPartsInModel;
		private System.Windows.Forms.Button buttonAddAllCategory;
		private System.Windows.Forms.Button button2;
		private System.Windows.Forms.Button buttonApplyMarks;
		private System.Windows.Forms.Button buttonApplyAllMarks;
		private System.Windows.Forms.DataGridView dataGridViewParts;
		private System.Windows.Forms.Label labelParts;
	}
}

