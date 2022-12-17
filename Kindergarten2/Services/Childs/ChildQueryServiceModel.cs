
namespace Kindergarten2.Services.Childs
{
	using System.Collections.Generic;

	public class ChildQueryServiceModel
	{
		public int CurrentPage { get; init; }

		public int ChildrenPerPage { get; init; }

		public int TotalChildren { get; init; }

		public IEnumerable<ChildServiceModel> Children { get; init; }
	}
}
