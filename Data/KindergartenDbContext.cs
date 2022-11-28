namespace Kindergarten2.Data
{
	using Kindergarten2.Data.Models;
	using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
	using Microsoft.EntityFrameworkCore;
	public class KindergartenDbContext : IdentityDbContext
	{
		public KindergartenDbContext(DbContextOptions<KindergartenDbContext> options)
			: base(options)
		{
		}
		protected override void OnModelCreating(ModelBuilder builder)
		{
			base.OnModelCreating(builder);
		}
		public DbSet<Category> Categories { get; init; }

		public DbSet<Child> Children { get; init; }

		public DbSet<ECA> ECAs { get; init; }

		public DbSet<Group> Groups { get; init; }

		public DbSet<Menu> Menus { get; init; }

		public DbSet<Teacher> Teachers { get; init; }

		public DbSet<Trip> Trips { get; init; }


	}
}
