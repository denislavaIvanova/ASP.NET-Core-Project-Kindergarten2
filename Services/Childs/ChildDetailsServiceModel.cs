namespace Kindergarten2.Services.Childs
{
	public class ChildDetailsServiceModel : ChildServiceModel
	{
		public int ParentId { get; init; }

		public string ParentFirstName { get; init; }

		public string ParentLastName { get; init; }

		public int GroupId { get; init; }

		public int ECAId { get; init; }

		public int MenuId { get; init; }

		public int TripId { get; init; }

		public string UserId { get; init; }

	}
}
