

namespace Kindergarten2.Controllers
{
	using Kindergarten2.Data;
	using Kindergarten2.Data.Models;
	using Kindergarten2.Models.ECAs;
	using Microsoft.AspNetCore.Authorization;
	using Microsoft.AspNetCore.Mvc;
	using System.Linq;

	public class ECAsController : Controller
	{
		private readonly KindergartenDbContext data;

		public ECAsController(KindergartenDbContext data)
				=> this.data = data;

		public IActionResult All([FromQuery] AllECAsQueryModel query)
		{
			var ECAsQuery = this.data.ECAs.AsQueryable();

			if (!string.IsNullOrWhiteSpace(query.Title))
			{
				ECAsQuery = ECAsQuery.Where(e => e.Title == query.Title);

			}

			if (!string.IsNullOrWhiteSpace(query.SearchTerm))
			{
				ECAsQuery = ECAsQuery.Where(e =>
				 e.Title.ToLower().Contains(query.SearchTerm.ToLower()) ||
				 e.Description.ToLower().Contains(query.SearchTerm.ToLower()));
			}

			ECAsQuery = query.Sorting switch
			{
				ECASorting.MonthlyFee => ECAsQuery.OrderBy(e => e.MonthlyFee),
				ECASorting.Title => ECAsQuery.OrderBy(e => e.Title),
				ECASorting.DateCreated or _ => ECAsQuery.OrderByDescending(e => e.Id)
			};

			var totalECAs = ECAsQuery.Count();

			var ECAs = ECAsQuery
				.Skip((query.CurrentPage - 1) * AllECAsQueryModel.ECAsPerPage)
				.Take(AllECAsQueryModel.ECAsPerPage)
				.Select(e => new ECAListingViewModel
				{
					Id = e.Id,
					Title = e.Title,
					Description = e.Description,
					MonthlyFee = e.MonthlyFee,
					ImageUrl = e.ImageUrl

				}).ToList();

			var ECAsTitles = this.data
				.ECAs
				.Select(e => e.Title)
				.Distinct()
				.OrderBy(t => t)
				.ToList();


			query.Titles = ECAsTitles;
			query.ECAs = ECAs;
			query.TotalECAs = totalECAs;

			return View(query);
		}

		[Authorize]

		public IActionResult Add() => View(new AddECAFormModel
		{

		});

		[HttpPost]
		[Authorize]

		public IActionResult Add(AddECAFormModel ECA)
		{
			if (!ModelState.IsValid)
			{
				return View(ECA);
			}

			var ECAData = new ECA
			{
				MonthlyFee = ECA.MonthlyFee,
				Title = ECA.Title,
				Description = ECA.Description,
				ImageUrl = ECA.ImageUrl
			};

			this.data.ECAs.Add(ECAData);

			this.data.SaveChanges();

			return RedirectToAction(nameof(All));
		}
	}
}
