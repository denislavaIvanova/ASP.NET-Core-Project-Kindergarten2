namespace Kindergarten2
{
	using Kindergarten2.Data;
	using Kindergarten2.Infrastructure;
	using Kindergarten2.Services.Statistics;
	using Microsoft.AspNetCore.Builder;
	using Microsoft.AspNetCore.Hosting;
	using Microsoft.AspNetCore.Identity;
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
				.AddDefaultIdentity<IdentityUser>(options =>
				{
					options.Password.RequireDigit = false;
					options.Password.RequireLowercase = false;
					options.Password.RequireNonAlphanumeric = false;
					options.Password.RequireUppercase = false;

				})
				.AddEntityFrameworkStores<KindergartenDbContext>();
			services.AddControllersWithViews();

			services.AddTransient<IStatisticsService, StatisticsService>();
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
