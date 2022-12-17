
namespace Kindergarten2.Controllers
{
	using Kindergarten2.Data;
	using Kindergarten2.Models.ECAs;
	using Kindergarten2.Services.ECAs;
	using Microsoft.AspNetCore.Authorization;
	using Microsoft.AspNetCore.Mvc;
	using static Kindergarten2.Areas.Admin.AdminConstants;


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

		[Authorize(Roles = AdministratorRoleName)]

		public IActionResult Add() => View(new AddECAFormModel
		{

		});

		[HttpPost]
		[Authorize(Roles = AdministratorRoleName)]


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

		[Authorize(Roles = AdministratorRoleName)]
		public IActionResult Edit(int id)
		{

			var ECA = this.ECAs.Details(id);


			return View(new ECAServiceModel
			{
				Title = ECA.Title,
				Description = ECA.Description,
				MonthlyFee = ECA.MonthlyFee,
				ImageUrl = ECA.ImageUrl
			});
		}

		[Authorize(Roles = AdministratorRoleName)]
		[HttpPost]

		public IActionResult Edit(int id, ECAServiceModel ECA)
		{
			if (!ModelState.IsValid)
			{
				return View(ECA);
			}

			this.ECAs.Edit(id,
				ECA.MonthlyFee,
				ECA.Title,
				ECA.Description,
				ECA.ImageUrl);

			return RedirectToAction(nameof(All));

		}

		[Authorize]

		public IActionResult Details(int id)
		{
			var ECA = this.ECAs.Details(id);

			return View(ECA);

		}


		[Authorize(Roles = AdministratorRoleName)]
		[HttpPost]
		[HttpDelete]
		public IActionResult Delete(int id)
		{

			this.ECAs.Delete(id);
			return RedirectToAction(nameof(All));
		}
	}
}
