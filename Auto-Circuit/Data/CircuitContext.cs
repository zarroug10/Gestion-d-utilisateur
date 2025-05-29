using Auto_Circuit.Entities;
using Auto_Circuit.Entities.identity;

using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Auto_Circuit.Data;

public class CircuitContext : IdentityDbContext<User, Role, string>
{
    public DbSet<Contract> Contracts { get; set; }
    public DbSet<Vacation> Vacations { get; set; }
    public DbSet<MonthlySpent> MonthlySpents { get; set; }
    public DbSet<WorkTime> WorkTimes { get; set; }
    public CircuitContext(DbContextOptions<CircuitContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

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
                new Role(UserType.RH, "1"),
                new Role(UserType.Responsable, "2"),
                new Role(UserType.Employe, "3"),
                new Role(UserType.Directeur, "4")
            );
    }
}
