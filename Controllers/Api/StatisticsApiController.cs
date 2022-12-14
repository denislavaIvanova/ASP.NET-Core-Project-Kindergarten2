﻿
namespace Kindergarten2.Controllers.Api
{
	using Kindergarten2.Services.Statistics;
	using Microsoft.AspNetCore.Mvc;

	[ApiController]
	[Route("api/statistics")]
	public class StatisticsApiController : ControllerBase
	{
		private readonly IStatisticsService statistics;

		public StatisticsApiController(IStatisticsService statistics)
			=> this.statistics = statistics;

		[HttpGet]

		public StatisticsServiceModel GetStatistics()
		=> this.statistics.Total();


	}
}
