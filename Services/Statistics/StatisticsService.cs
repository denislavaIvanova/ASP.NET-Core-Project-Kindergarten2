
namespace Kindergarten2.Services.Statistics
{
	using Kindergarten2.Data;
	using System.Linq;

	public class StatisticsService : IStatisticsService
	{
		private readonly KindergartenDbContext data;

		public StatisticsService(KindergartenDbContext data)
			=> this.data = data;
		public StatisticsServiceModel Total()
		{
			var totalTeachers = this.data.Teachers.Count();
			var totalChildren = this.data.Children.Count(c => c.IsConfirmed);
			var totalGroups = this.data.Groups.Count();

			return new StatisticsServiceModel
			{
				TotalTeachers = totalTeachers,
				TotalChildren = totalChildren,
				TotalGroups = totalGroups
			};
		}
	}
}
