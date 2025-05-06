using Auto_Circuit.Entities;
using Auto_Circuit.Entities.identity;

using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Auto_Circuit.Data;

public class CircuitContext : IdentityDbContext<User, Role, string>
{
    public DbSet<Product> Products { get; set; }
    public DbSet<Rating> Ratings { get; set; }
    public DbSet<Review> Reviews { get; set; }
    public DbSet<UserProducts> UserProducts { get; set; }
    public DbSet<UserRole> UserRoles { get; set; }

    public CircuitContext(DbContextOptions<CircuitContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.Entity<UserProducts>()
            .HasKey(up => new { up.UserId, up.ProductId });

        builder.Entity<UserProducts>()
            .HasOne(up => up.User)
            .WithMany(u => u.UserProducts)
            .HasForeignKey(up => up.UserId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.Entity<UserProducts>()
            .HasOne(d => d.Product)
            .WithMany(p => p.UserProducts)
            .HasForeignKey(d => d.ProductId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.Entity<User>()
            .HasMany(u => u.UserRoles)
            .WithOne(up => up.User)
            .HasForeignKey(up => up.UserId);


        builder.Entity<Role>()
            .HasMany(r => r.UserRoles)
            .WithOne(up => up.Role)
            .HasForeignKey(up => up.RoleId);

        builder.Entity<Role>()
            .HasData(
                new Role(UserType.Account, "1"),
                new Role(UserType.Admin, "2")
            );
    }
}
