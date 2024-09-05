using Azure;
using Demo_Pattern_Repository.Models;
using Microsoft.EntityFrameworkCore;

namespace Demo_Pattern_Repository.DatabaseEFCore.Config
{
    public class AnimalDbContext : DbContext
    {
        public DbSet<Animal> Animals { get; set; }
        public DbSet<Familia> Familias { get; set; }
        public DbSet<Region> Regions { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=localhost;Database=Demo_Pattern_Repository;Trusted_Connection=True;Trust Server Certificate=True");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Animal>().ToTable("Animal");
            modelBuilder.Entity<Region>().ToTable("Region");
            modelBuilder.Entity<Familia>().ToTable("Familia");

            modelBuilder.Entity<Animal>().Property(a => a.IsDomesticated)
                                         .HasColumnName("Domesticated");

            modelBuilder.Entity<Familia>().Property(f => f.Desc)
                                          .HasColumnName("Description");

            modelBuilder.Entity<Animal>()
                        .HasOne(a => a.Familia);

            modelBuilder.Entity<Animal>()
                        .HasMany(a => a.Regions)
                        .WithMany(r => r.Animals)
                        .UsingEntity("Animal_Region",
                                    l => l.HasOne(typeof(Region)).WithMany().HasForeignKey("RegionId").HasPrincipalKey(nameof(Region.Id)),
                                    r => r.HasOne(typeof(Animal)).WithMany().HasForeignKey("AnimalId").HasPrincipalKey(nameof(Animal.Id)),
                                    j => j.HasKey("AnimalId", "RegionId"));
        }
    }
}
