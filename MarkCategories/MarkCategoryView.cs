namespace MarkCategories
{
	public partial class MarkCategoriesForm
	{
		public class MarkCategoryView
		{
			public int CatId { get; set; }
			public string Description { get; set; }
			public string A_Prefix { get; set; }
			public int A_Number { get; set; }
			public string P_Prefix { get; set; }
			public int P_Number { get; set; }
			public bool HasPhase { get; set; }
			public bool HasDash { get; set; }
			public string Mark => this.ToString();
			public int Quantity { get; set; }

			public override string ToString()
			{
				var mark = $"{A_Prefix}{A_Number}";
				if (HasPhase && !HasDash)
				{
					mark = $"1{A_Prefix}{A_Number}";
				}
				if (HasPhase && HasDash)
				{
					mark = $"1{A_Prefix}-{A_Number}";
				}
				return mark;
			}

			public MarkCategoryView(MarkCategory m)
			{
				CatId = m.Id;
				Description = m.Description;
				A_Prefix = m.AssemblyNumber.Prefix;
				A_Number = m.AssemblyNumber.StartNumber;
				P_Prefix = m.PartNumber.Prefix;
				P_Number = m.PartNumber.StartNumber;
				HasPhase = m.HasPhase;
				HasDash = m.HasDash;
				Quantity = m.Parts.Count;
			}
		}
	}
}
