using System.Security.Claims;

namespace Kindergarten2.Infrastructure
{
	public static class ClaimsPrincipalExtensions
	{
		public static string GetId(this ClaimsPrincipal user)
			=> user.FindFirst(ClaimTypes.NameIdentifier).Value;
	}
}
