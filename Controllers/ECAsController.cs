

namespace Kindergarten2.Controllers
{
	using Kindergarten2.Data;
	using Kindergarten2.Models.ECAs;
	using Kindergarten2.Services.ECAs;
	using Microsoft.AspNetCore.Authorization;
	using Microsoft.AspNetCore.Mvc;

	public class ECAsController : Controller
	{
		private readonly KindergartenDbContext data;
		private readonly IECAService ECAs;


		public ECAsController(KindergartenDbContext data, IECAService ECAs)
		{
			this.data = data;
			this.ECAs = ECAs;
		}

		public IActionResult All([FromQuery] AllECAsQueryModel query)
		{
			var queryResult = this.ECAs.All
				(query.Title,
				query.SearchTerm,
				query.Sorting,
				query.CurrentPage,
				AllECAsQueryModel.ECAsPerPage);

			var ECAsTitles = this.ECAs.AllECAsTitles();


			query.ECAs = queryResult.ECAs;
			query.Titles = ECAsTitles;
			query.TotalECAs = queryResult.TotalECAs;

			return View(query);
		}

		[Authorize(Roles = "Administrator")]

		public IActionResult Add() => View(new AddECAFormModel
		{

		});

		[HttpPost]
		[Authorize(Roles = "Administrator")]


		public IActionResult Add(AddECAFormModel ECA)
		{
			if (!ModelState.IsValid)
			{
				return View(ECA);
			}

			this.ECAs.Create(ECA.MonthlyFee,
				ECA.Title,
				ECA.Description,
				ECA.ImageUrl);

			return RedirectToAction(nameof(All));
		}
	}
}
