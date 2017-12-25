using System.Collections.Generic;
using Tekla.Structures.Model;

namespace MarkCategories
{
	public partial class MarkCategoriesForm
	{
		public class MarkCategory
		{
			public int Id { get; set; }
			public string Description { get; set; }
			public NumberingSeries AssemblyNumber { get; set; }
			public NumberingSeries PartNumber { get; set; }
			public bool HasPhase { get; set; }
			public bool HasDash { get; set; }
			public List<Part> Parts { get; set; }
		}
	}
}
