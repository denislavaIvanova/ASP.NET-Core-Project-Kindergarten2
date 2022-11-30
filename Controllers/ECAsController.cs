

namespace Kindergarten2.Controllers
{
	using Kindergarten2.Data;
	using Kindergarten2.Data.Models;
	using Kindergarten2.Models.ECAs;
	using Microsoft.AspNetCore.Mvc;

	public class ECAsController : Controller
	{
		private readonly KindergartenDbContext data;

		public ECAsController(KindergartenDbContext data)
				=> this.data = data;
		public IActionResult Add() => View(new AddECAFormModel
		{

		});

		[HttpPost]

		public IActionResult Add(AddECAFormModel ECA)
		{
			if (!ModelState.IsValid)
			{
				return View(ECA);
			}

			var ECAData = new ECA
			{
				MonthlyFee = ECA.MonthlyFee,
				Description = ECA.Description,
			};

			this.data.ECAs.Add(ECAData);

			this.data.SaveChanges();

			return RedirectToAction("Index", "Home");
		}
	}
}
