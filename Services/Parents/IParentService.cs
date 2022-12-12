namespace Kindergarten2.Services.Parents
{
	public interface IParentService
	{
		public bool IsParent(string userId);

		public int IdByUser(string userId);
	}
}
