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
			((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.dataGridViewMarkCategories)).BeginInit();
			this.SuspendLayout();
			// 
			// pictureBox1
			// 
			this.pictureBox1.Image = global::MarkCategories.Properties.Resources.Logo;
			this.pictureBox1.Location = new System.Drawing.Point(13, 13);
			this.pictureBox1.Name = "pictureBox1";
			this.pictureBox1.Size = new System.Drawing.Size(160, 51);
			this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
			this.pictureBox1.TabIndex = 0;
			this.pictureBox1.TabStop = false;
			// 
			// dataGridViewMarkCategories
			// 
			this.dataGridViewMarkCategories.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.DisplayedCells;
			this.dataGridViewMarkCategories.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			this.dataGridViewMarkCategories.Location = new System.Drawing.Point(13, 131);
			this.dataGridViewMarkCategories.Name = "dataGridViewMarkCategories";
			this.dataGridViewMarkCategories.RowTemplate.Height = 24;
			this.dataGridViewMarkCategories.Size = new System.Drawing.Size(990, 230);
			this.dataGridViewMarkCategories.TabIndex = 1;
			this.dataGridViewMarkCategories.CellEndEdit += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridViewMarkCategories_CellEndEdit);
			// 
			// buttonAddMarkCategory
			// 
			this.buttonAddMarkCategory.Location = new System.Drawing.Point(751, 92);
			this.buttonAddMarkCategory.Name = "buttonAddMarkCategory";
			this.buttonAddMarkCategory.Size = new System.Drawing.Size(123, 33);
			this.buttonAddMarkCategory.TabIndex = 2;
			this.buttonAddMarkCategory.Text = "Add";
			this.buttonAddMarkCategory.UseVisualStyleBackColor = true;
			this.buttonAddMarkCategory.Click += new System.EventHandler(this.buttonAddMarkCategory_Click);
			// 
			// buttonSelect
			// 
			this.buttonSelect.Location = new System.Drawing.Point(880, 92);
			this.buttonSelect.Name = "buttonSelect";
			this.buttonSelect.Size = new System.Drawing.Size(123, 33);
			this.buttonSelect.TabIndex = 4;
			this.buttonSelect.Text = "Select";
			this.buttonSelect.UseVisualStyleBackColor = true;
			this.buttonSelect.Click += new System.EventHandler(this.buttonSelect_Click);
			// 
			// buttonSave
			// 
			this.buttonSave.Location = new System.Drawing.Point(880, 367);
			this.buttonSave.Name = "buttonSave";
			this.buttonSave.Size = new System.Drawing.Size(123, 33);
			this.buttonSave.TabIndex = 5;
			this.buttonSave.Text = "Save";
			this.buttonSave.UseVisualStyleBackColor = true;
			this.buttonSave.Click += new System.EventHandler(this.buttonSave_Click);
			// 
			// buttonLoad
			// 
			this.buttonLoad.Location = new System.Drawing.Point(751, 367);
			this.buttonLoad.Name = "buttonLoad";
			this.buttonLoad.Size = new System.Drawing.Size(123, 33);
			this.buttonLoad.TabIndex = 6;
			this.buttonLoad.Text = "Load";
			this.buttonLoad.UseVisualStyleBackColor = true;
			this.buttonLoad.Click += new System.EventHandler(this.buttonLoad_Click);
			// 
			// MarkCategoriesForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(1015, 405);
			this.Controls.Add(this.buttonLoad);
			this.Controls.Add(this.buttonSave);
			this.Controls.Add(this.buttonSelect);
			this.Controls.Add(this.buttonAddMarkCategory);
			this.Controls.Add(this.dataGridViewMarkCategories);
			this.Controls.Add(this.pictureBox1);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.Name = "MarkCategoriesForm";
			this.Text = "Mark Categories";
			this.Load += new System.EventHandler(this.MarkCategoriesForm_Load);
			((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.dataGridViewMarkCategories)).EndInit();
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.PictureBox pictureBox1;
		private System.Windows.Forms.DataGridView dataGridViewMarkCategories;
		private System.Windows.Forms.Button buttonAddMarkCategory;
		private System.Windows.Forms.Button buttonSelect;
		private System.Windows.Forms.Button buttonSave;
		private System.Windows.Forms.Button buttonLoad;
	}
}

