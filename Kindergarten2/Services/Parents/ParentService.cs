
namespace Kindergarten2.Services.Parents
{
	using Kindergarten2.Data;
	using System.Linq;
	public class ParentService : IParentService
	{
		private readonly KindergartenDbContext data;

		public ParentService(KindergartenDbContext data)
			=> this.data = data;
		public bool IsParent(string userId)
			=> this.data
			.Parents
			.Any(p => p.UserId == userId);

		public int IdByUser(string userId)
			=> this.data
				.Parents
				.Where(p => p.UserId == userId)
				.Select(p => p.Id)
				.FirstOrDefault();
	}
}
