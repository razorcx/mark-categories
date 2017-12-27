using System.Collections.Generic;
using Tekla.Structures.Model;

namespace MarkCategories
{
	public class MarkCategory
	{
		public int Id { get; set; }
		public string Name { get; set; }
		public NumberingSeries AssemblyNumber { get; set; }
		public NumberingSeries PartNumber { get; set; }
		public bool HasPhase { get; set; }
		public bool HasDash { get; set; }
		public List<RazorPart> Parts { get; set; }
	}
}
