using GloveYourself.Data.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace GloveYourself.Data.Data;

public class ApplicationDbContext : IdentityDbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    public DbSet<GloveEntity> Gloves { get; set; }

    public DbSet<CategoryEntity> Categories { get; set; }

    //protected override void OnModelCreating(ModelBuilder modelBuilder)
    //{
    //    modelBuilder.Entity<GloveEntity>()
    //        .HasOne(b => b.CategoryEntity)
    //        .WithOne(i => i.GloveEntity)
    //        .HasForeignKey<CategoryEntity>(b => b.GloveForeignKey);
    //}
}

