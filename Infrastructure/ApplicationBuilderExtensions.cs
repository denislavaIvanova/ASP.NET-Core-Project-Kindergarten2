
namespace Kindergarten2.Infrastructure
{
	using Kindergarten2.Data;
	using Kindergarten2.Data.Models;
	using Microsoft.AspNetCore.Builder;
	using Microsoft.EntityFrameworkCore;
	using Microsoft.Extensions.DependencyInjection;
	using System.Linq;

	public static class ApplicationBuilderExtensions
	{
		public static IApplicationBuilder PrepareDatabase(
			this IApplicationBuilder app)
		{
			using var scopedServices = app.ApplicationServices.CreateScope();

			var data = scopedServices.ServiceProvider.GetService<KindergartenDbContext>();

			data.Database.Migrate();

			SeedCategories(data);


			return app;
		}

		private static void SeedCategories(KindergartenDbContext data)
		{
			if (data.Categories.Any())
			{
				return;
			}

			data.Categories.AddRange(new[]
			{
				new Category{Description="Nursery" },
				new Category{Description="Kindergarten" },
				new Category{Description="Prepschool" },

			});

			data.SaveChanges();
		}
	}
}
