
namespace Kindergarten2.Services.ECAs
{
	using Kindergarten2.Models.ECAs;
	using System.Collections.Generic;

	public interface IECAService
	{
		ECAQueryServiceModel All(string title,
			string searchTerm,
			ECASorting sorting,
			int currentPage,
			int ECAsPerPage);

		IEnumerable<string> AllECAsTitles();

		int Create(double monthlyFee,
			   string title,
			   string description,
			   string imageUrl);
		bool Edit(int id,
			double monthlyFee,
				   string title,
				   string description,
				   string imageUrl);

		ECAServiceModel Details(int id);

		void Delete(int id);
	}
}
