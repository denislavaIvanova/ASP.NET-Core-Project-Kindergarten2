
namespace Kindergarten2.Infrastructure
{
	using Kindergarten2.Services.Childs;

	public static class ModelExtensions
	{
		public static string GetInformation(this IChildModel child)
			=> child.FirstName + " - " + child.LastName;
	}
}
