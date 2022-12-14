namespace Kindergarten2.Controllers
{
	using Kindergarten2.Models.Home;
	using Kindergarten2.Services.Statistics;
	using Kindergarten2.Services.Teachers;
	using Microsoft.AspNetCore.Mvc;
	using Microsoft.Extensions.Caching.Memory;
	using System;
	using System.Collections.Generic;

	public class HomeController : Controller
	{

		private readonly ITeacherService teachers;
		private readonly IStatisticsService statistics;
		private readonly IMemoryCache cache;


		public HomeController(
			IStatisticsService statistics, ITeacherService teachers, IMemoryCache cache)
		{
			this.statistics = statistics;
			this.teachers = teachers;
			this.cache = cache;
		}

		const string latestTeachersCacheKey = "LatestTeachersCacheKey";



		public IActionResult Index()
		{
			var latestTeachers = this.cache.Get<List<LatestTeacherServiceModel>>(latestTeachersCacheKey);

			if (latestTeachers == null)
			{
				latestTeachers = this.teachers.Latest();

				var cacheOptions = new MemoryCacheEntryOptions()
				   .SetAbsoluteExpiration(TimeSpan.FromMinutes(10));

				this.cache.Set(latestTeachersCacheKey, latestTeachers, cacheOptions);
			}



			var totalStatistics = this.statistics.Total();

			return View(new IndexViewModel
			{
				TotalTeachers = totalStatistics.TotalTeachers,
				Teachers = latestTeachers,
				TotalChildren = totalStatistics.TotalChildren,
				TotalGroups = totalStatistics.TotalGroups
			});

		}

		public IActionResult Error() => View();

	}
}
