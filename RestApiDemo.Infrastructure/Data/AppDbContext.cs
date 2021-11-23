using Microsoft.EntityFrameworkCore;
using RestApiDemo.DomainCore.Models.Resource;

namespace RestApiDemo.Infrastructure.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> opt) : base(opt) { }

        public DbSet<Resource> Resources { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ResourceContent>(
                entity =>
                {
                    entity.HasKey(x=>x.Id);
                    entity.ToTable("ResourceContent");
                    entity.HasOne(x => x.Resource)
                          .WithMany(y => y.ResourceContents)
                          .HasForeignKey(x => x.ResourceId)
                          .OnDelete(DeleteBehavior.Cascade)
                          .HasConstraintName("FK_ResourceContent_Resources_ResourceId");
                }
            );

            modelBuilder.Entity<Resource>(
                entity =>
                {
                    entity.ToTable("Resources").HasKey(x => x.Id);
                }
            );

        }

        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    if (!optionsBuilder.IsConfigured)
        //    {
        //        optionsBuilder.UseSqlServer("Server=localhost;Initial Catalog=RestApiDemo;User Id=demoUser;Password=demoUser");
        //    }
        //}
    }
}
