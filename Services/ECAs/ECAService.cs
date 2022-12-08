using Kindergarten2.Data;
using Kindergarten2.Models.ECAs;
using System.Collections.Generic;
using System.Linq;

namespace Kindergarten2.Services.ECAs
{
	public class ECAService : IECAService
	{
		private readonly KindergartenDbContext data;

		public ECAService(KindergartenDbContext data)
			=> this.data = data;
		public ECAQueryServiceModel All(string title, string searchTerm, ECASorting sorting, int currentPage, int ECAsPerPage)
		{
			var ECAsQuery = this.data.ECAs.AsQueryable();

			if (!string.IsNullOrWhiteSpace(title))
			{
				ECAsQuery = ECAsQuery.Where(e => e.Title == title);

			}

			if (!string.IsNullOrWhiteSpace(searchTerm))
			{
				ECAsQuery = ECAsQuery.Where(e =>
				 e.Title.ToLower().Contains(searchTerm.ToLower()) ||
				 e.Description.ToLower().Contains(searchTerm.ToLower()));
			}

			ECAsQuery = sorting switch
			{
				ECASorting.MonthlyFee => ECAsQuery.OrderBy(e => e.MonthlyFee),
				ECASorting.Title => ECAsQuery.OrderBy(e => e.Title),
				ECASorting.DateCreated or _ => ECAsQuery.OrderByDescending(e => e.Id)
			};

			var totalECAs = ECAsQuery.Count();

			var ECAs = ECAsQuery
				.Skip((currentPage - 1) * ECAsPerPage)
				.Take(ECAsPerPage)
				.Select(e => new ECAServiceModel
				{
					Id = e.Id,
					Title = e.Title,
					Description = e.Description,
					MonthlyFee = e.MonthlyFee,
					ImageUrl = e.ImageUrl

				}).ToList();

			return new ECAQueryServiceModel
			{
				TotalECAs = totalECAs,
				ECAsPerPage = ECAsPerPage,
				CurrentPage = currentPage,
				ECAs = ECAs
			};
		}

		public IEnumerable<string> AllECAsTitles()
			=> this.data
				.ECAs
				.Select(e => e.Title)
				.Distinct()
				.OrderBy(t => t)
				.ToList();
	}
}
