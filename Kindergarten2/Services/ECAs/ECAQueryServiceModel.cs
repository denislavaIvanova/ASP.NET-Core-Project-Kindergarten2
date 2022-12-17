namespace Kindergarten2.Services.ECAs
{
	using System.Collections.Generic;
	public class ECAQueryServiceModel
	{
		public int CurrentPage { get; init; }

		public int ECAsPerPage { get; init; }

		public int TotalECAs { get; init; }

		public IEnumerable<ECAServiceModel> ECAs { get; init; }
	}
}
