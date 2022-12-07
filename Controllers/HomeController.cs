namespace Kindergarten2.Controllers
{
	using Kindergarten2.Data;
	using Kindergarten2.Models;
	using Kindergarten2.Models.Home;
	using Kindergarten2.Services.Statistics;
	using Microsoft.AspNetCore.Mvc;
	using System.Diagnostics;
	using System.Linq;

	public class HomeController : Controller
	{

		private readonly KindergartenDbContext data;
		private readonly IStatisticsService statistics;

		public HomeController(
			IStatisticsService statistics, KindergartenDbContext data)
		{
			this.statistics = statistics;
			this.data = data;
		}
		public IActionResult Index()
		{

			var teachers = this.data
					.Teachers
					.OrderBy(t => t.FirstName)
					.ThenBy(t => t.LastName)
					.Select(t => new TeacherIndexViewModel
					{
						Id = t.Id,
						FirstName = t.FirstName,
						LastName = t.LastName,
						Experience = t.Experience,
						Specialization = t.Specialization,
						Introduction = t.Introduction,
						ImageUrl = t.ImageUrl
					})
					.Take(3)
					.ToList();

			var totalStatistics = this.statistics.Total();

			return View(new IndexViewModel
			{
				TotalTeachers = totalStatistics.TotalTeachers,
				Teachers = teachers,
				TotalChildren = totalStatistics.TotalChildren,
				TotalGroups = totalStatistics.TotalGroups
			});

		}

		[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
		public IActionResult Error() => View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });

	}
}
