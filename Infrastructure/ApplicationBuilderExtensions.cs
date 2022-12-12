
namespace Kindergarten2.Infrastructure
{
	using Kindergarten2.Data;
	using Kindergarten2.Data.Models;
	using Microsoft.AspNetCore.Builder;
	using Microsoft.AspNetCore.Identity;
	using Microsoft.EntityFrameworkCore;
	using Microsoft.Extensions.DependencyInjection;
	using System;
	using System.Linq;
	using System.Threading.Tasks;
	using static WebConstants;

	public static class ApplicationBuilderExtensions
	{
		public static IApplicationBuilder PrepareDatabase(
			this IApplicationBuilder app)
		{
			using var scopedServices = app.ApplicationServices.CreateScope();

			var serviceProvider = scopedServices.ServiceProvider;

			var data = serviceProvider.GetRequiredService<KindergartenDbContext>();

			data.Database.Migrate();

			SeedCategories(data);

			SeedGroups(data);

			SeedAdministrator(serviceProvider);


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
				new Category{Description="Music" },
				new Category{Description="Engineering" },
				new Category{Description="IT" },

			});

			data.SaveChanges();
		}

		private static void SeedGroups(KindergartenDbContext data)
		{
			if (data.Groups.Any())
			{
				return;
			}

			data.Groups.AddRange(new[]
			{
				new Group{Name="Snowdrop",ChildrenCount=19,CategoryId=1 },
				new Group{Name="Daisy" ,ChildrenCount=15,CategoryId=1},
				new Group{Name="Dew",ChildrenCount=14,CategoryId=2 },
				new Group{Name="Bear",ChildrenCount=16,CategoryId=2 },
				new Group{Name="Sweet",ChildrenCount=12,CategoryId=2 },
				new Group{Name="Chocolate",ChildrenCount=5,CategoryId=3 },
				new Group{Name="Rose",ChildrenCount=7,CategoryId=3 },


			});

			data.SaveChanges();
		}

		private static void SeedAdministrator(IServiceProvider services)
		{

			var userManager = services.GetRequiredService<UserManager<User>>();
			var roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();

			Task
				.Run(async () =>
				{
					if (await roleManager.RoleExistsAsync(AdministratorRoleName))
					{
						return;
					}

					var role = new IdentityRole { Name = AdministratorRoleName };

					await roleManager.CreateAsync(role);

					const string adminEmail = "admin@kdr.com";
					const string adminPassword = "admin1234";


					var user = new User
					{
						Email = adminEmail,
						UserName = adminEmail,
						FullName = "Admin"
					};

					await userManager.CreateAsync(user, adminPassword);

					await userManager.AddToRoleAsync(user, role.Name);
				})
				.GetAwaiter()
				.GetResult();

		}
	}
}
