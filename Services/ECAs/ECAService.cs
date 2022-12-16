

namespace Kindergarten2.Services.ECAs
{
	using Kindergarten2.Data;
	using Kindergarten2.Data.Models;
	using Kindergarten2.Models.ECAs;
	using System.Collections.Generic;
	using System.Linq;
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

		public int Create(double monthlyFee, string title, string description, string imageUrl)
		{
			var ECAData = new ECA
			{
				MonthlyFee = monthlyFee,
				Title = title,
				Description = description,
				ImageUrl = imageUrl
			};

			this.data.ECAs.Add(ECAData);

			this.data.SaveChanges();

			return ECAData.Id;
		}

		public bool Edit(int id, double monthlyFee, string title, string description, string imageUrl)
		{
			var ECAData = this.data.ECAs.Find(id);

			if (ECAData == null)
			{
				return false;
			}

			ECAData.MonthlyFee = monthlyFee;
			ECAData.Title = title;
			ECAData.Description = description;
			ECAData.ImageUrl = imageUrl;

			this.data.SaveChanges();

			return true;
		}

		public ECAServiceModel Details(int id)
			=> this.data
			.ECAs.Where(e => e.Id == id)
			.Select(e => new ECAServiceModel
			{
				Id = e.Id,
				Title = e.Title,
				Description = e.Description,
				MonthlyFee = e.MonthlyFee,
				ImageUrl = e.ImageUrl

			}).FirstOrDefault();

		public void Delete(int id)
		{
			var ECAToDelete = this.data.ECAs.Find(id);

			this.data.ECAs.Remove(ECAToDelete);

			this.data.SaveChanges();
		}
	}
}
