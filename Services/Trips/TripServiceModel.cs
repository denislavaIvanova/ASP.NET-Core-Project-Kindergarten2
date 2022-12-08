namespace Kindergarten2.Services.Trips
{
	public class TripServiceModel
	{
		public int Id { get; init; }

		public string PlaceToVisit { get; set; }

		public string Activity { get; set; }

		public string ImageUrl { get; set; }

		public double Price { get; set; }

		public string Location { get; set; }
	}
}
