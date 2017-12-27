using System.Collections.Generic;
using Newtonsoft.Json;
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
			public List<RazorPart> Parts { get; set; }
		}
	}

	public class RazorPart
	{
		public string Name { get; set; }
		public NumberingSeries AssemblyNumber { get; set; }
		public NumberingSeries PartNumber { get; set; }
		public DeformingData DeformingData { get; set; }
		public Material Material { get; set; }
		public Profile Profile { get; set; }
		public Position Position { get; set; }
		public string Class { get; set; }
		public int PourPhase { get; set; }
		public string Finish { get; set; }
	}
}
