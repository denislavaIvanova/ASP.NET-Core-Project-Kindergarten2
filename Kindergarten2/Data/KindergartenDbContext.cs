namespace Kindergarten2.Data
{
	using Kindergarten2.Data.Models;
	using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
	using Microsoft.EntityFrameworkCore;
	public class KindergartenDbContext : IdentityDbContext<User>
	{
		public KindergartenDbContext(DbContextOptions<KindergartenDbContext> options)
			: base(options)
		{
		}
		protected override void OnModelCreating(ModelBuilder builder)
		{
			builder
			   .Entity<Child>()
			   .HasOne(c => c.Group)
			   .WithMany(c => c.Children)
			   .HasForeignKey(c => c.GroupId)
			   .OnDelete(DeleteBehavior.Restrict);

			builder
		   .Entity<Child>()
		   .HasOne(c => c.ECA)
		   .WithMany(c => c.Children)
		   .HasForeignKey(c => c.ECAId)
		   .OnDelete(DeleteBehavior.Restrict);

			builder
		   .Entity<Child>()
		   .HasOne(c => c.Menu)
		   .WithMany(c => c.Children)
		   .HasForeignKey(c => c.MenuId)
		   .OnDelete(DeleteBehavior.Restrict);

			builder
		   .Entity<Child>()
		   .HasOne(c => c.Trip)
		   .WithMany(c => c.Children)
		   .HasForeignKey(c => c.TripId)
		   .OnDelete(DeleteBehavior.Restrict);



			builder
			.Entity<Child>()
			.HasOne(c => c.Parent)
			.WithMany(p => p.Children)
			.HasForeignKey(c => c.ParentId)
			.OnDelete(DeleteBehavior.Restrict);

			builder
			.Entity<Parent>()
			.HasOne<User>()
			.WithOne()
			.HasForeignKey<Parent>(p => p.UserId)
			.OnDelete(DeleteBehavior.Restrict);

			base.OnModelCreating(builder);
		}
		public DbSet<Category> Categories { get; init; }

		public DbSet<Child> Children { get; init; }

		public DbSet<ECA> ECAs { get; init; }

		public DbSet<Group> Groups { get; init; }

		public DbSet<Menu> Menus { get; init; }

		public DbSet<Teacher> Teachers { get; init; }

		public DbSet<Trip> Trips { get; init; }

		public DbSet<Parent> Parents { get; init; }




	}
}
