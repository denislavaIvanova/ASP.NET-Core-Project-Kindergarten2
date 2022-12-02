namespace Kindergarten2.Controllers
{
	using Kindergarten2.Data;
	using Kindergarten2.Models;
	using Kindergarten2.Models.Teachers;
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
			var teachers = this.data
					.Teachers
					.OrderBy(t => t.FirstName)
					.ThenBy(t => t.LastName)
					.Select(t => new TeacherListingViewModel
					{
						Id = t.Id,
						FirstName = t.FirstName,
						LastName = t.LastName,
						Experience = t.Experience,
						Specialization = t.Specialization,
						Group = t.Group.Name,
						ImageUrl = t.ImageUrl
					})
					.Take(3)
					.ToList();

			return View(teachers);

		}

		[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
		public IActionResult Error() => View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });

	}
}
