namespace Kindergarten2.Controllers
{
	using Kindergarten2.Data;
	using Kindergarten2.Models;
	using Kindergarten2.Models.Home;
	using Microsoft.AspNetCore.Mvc;
	using System.Diagnostics;
	using System.Linq;

	public class HomeController : Controller
	{

		private readonly KindergartenDbContext data;

		public HomeController(KindergartenDbContext data)
			=> this.data = data;
		public IActionResult Index()
		{

			var totalTeachers = this.data.Teachers.Count();
			var totalChildren = this.data.Children.Count();
			var totalGroups = this.data.Groups.Count();

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

			return View(new IndexViewModel
			{
				TotalTeachers = totalTeachers,
				Teachers = teachers,
				TotalChildren = totalChildren,
				TotalGroups = totalGroups
			});

		}

		[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
		public IActionResult Error() => View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });

	}
}
