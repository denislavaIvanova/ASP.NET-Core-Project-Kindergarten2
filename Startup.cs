namespace Kindergarten2
{
	using Kindergarten2.Data;
	using Kindergarten2.Data.Models;
	using Kindergarten2.Infrastructure;
	using Kindergarten2.Services.Childs;
	using Kindergarten2.Services.ECAs;
	using Kindergarten2.Services.Menus;
	using Kindergarten2.Services.Parents;
	using Kindergarten2.Services.Statistics;
	using Kindergarten2.Services.Teachers;
	using Kindergarten2.Services.Trips;
	using Microsoft.AspNetCore.Builder;
	using Microsoft.AspNetCore.Hosting;
	using Microsoft.AspNetCore.Identity;
	using Microsoft.AspNetCore.Mvc;
	using Microsoft.EntityFrameworkCore;
	using Microsoft.Extensions.Configuration;
	using Microsoft.Extensions.DependencyInjection;
	using Microsoft.Extensions.Hosting;
	public class Startup
	{
		public Startup(IConfiguration configuration)
			=> Configuration = configuration;


		public IConfiguration Configuration { get; }


		public void ConfigureServices(IServiceCollection services)
		{
			services.AddDbContext<KindergartenDbContext>(options =>
				options.UseSqlServer(
					Configuration.GetConnectionString("DefaultConnection")));
			services.AddDatabaseDeveloperPageExceptionFilter();

			services
				.AddDefaultIdentity<User>(options =>
				{
					options.Password.RequireDigit = false;
					options.Password.RequireLowercase = false;
					options.Password.RequireNonAlphanumeric = false;
					options.Password.RequireUppercase = false;
					options.SignIn.RequireConfirmedAccount = false;

				})
				.AddRoles<IdentityRole>()
				.AddEntityFrameworkStores<KindergartenDbContext>();

			services.AddControllersWithViews(options =>
			{
				options.Filters.Add<AutoValidateAntiforgeryTokenAttribute>();
			});

			services.AddTransient<IStatisticsService, StatisticsService>();
			services.AddTransient<ITeacherService, TeacherService>();
			services.AddTransient<ITripService, TripService>();
			services.AddTransient<IMenuService, MenuService>();
			services.AddTransient<IECAService, ECAService>();
			services.AddTransient<IChildService, ChildService>();
			services.AddTransient<IParentService, ParentService>();


		}


		public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
		{
			app.PrepareDatabase();

			if (env.IsDevelopment())
			{
				app.UseDeveloperExceptionPage();
				app.UseMigrationsEndPoint();
			}
			else
			{
				app.UseExceptionHandler("/Home/Error");
				app.UseHsts();
			}
			app
				.UseHttpsRedirection()
				.UseStaticFiles()
				.UseRouting()
				.UseAuthentication()
				.UseAuthorization()
				.UseEndpoints(endpoints =>
				{
					endpoints.MapDefaultControllerRoute();
					endpoints.MapRazorPages();
				});
		}
	}
}
