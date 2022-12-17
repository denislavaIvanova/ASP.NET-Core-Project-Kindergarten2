

namespace Kindergarten2.Test.Mocks
{
	using Kindergarten2.Services.Statistics;
	using Moq;

	public static class StatisticsServiceMock
	{

		public static IStatisticsService Instance
		{
			get
			{
				var statisticsServiceMock = new Mock<IStatisticsService>();

				statisticsServiceMock.Setup(s => s.Total())
					.Returns(new StatisticsServiceModel
					{
						TotalChildren = 5,
						TotalGroups = 10,
						TotalTeachers = 15

					});

				return statisticsServiceMock.Object;
			}

		}
	}
}
