using Microsoft.EntityFrameworkCore;
using SamuraiApp.Domain;

namespace SamuraiApp.Data
{
	public class SamuraiContext : DbContext
	{
		public DbSet<Samurai> Samurais { get; set; }
		public DbSet<Quote> Quotes { get; set; }
		public DbSet<Battle> Battles { get; set; }

		protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
		{
			optionsBuilder.UseSqlServer("Server=(localdb)\\MSSQLLocalDB;Database=SamuraiAppData;Trusted_Connection=true;");
			base.OnConfiguring(optionsBuilder);
		}

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			modelBuilder.Entity<Samurai>()
				.HasMany(s => s.Battles)
				.WithMany(b => b.Samurais)
				.UsingEntity<BattleSamurai>
				(bs => bs.HasOne<Battle>().WithMany(),
				bs => bs.HasOne<Samurai>().WithMany())
				.Property(bs => bs.DateJoined)
				.HasDefaultValueSql("getdate()");
		}
	}
}