using GloveYourself.Data.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace GloveYourself.Data.Data;

public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    public DbSet<GloveEntity> Gloves { get; set; }

    public DbSet<CategoryEntity> Categories { get; set; }

    public DbSet <TaskEntity> Tasks { get; set; }
}

