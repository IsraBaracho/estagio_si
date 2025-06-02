using Microsoft.EntityFrameworkCore;
using FeevAtend.Domain.Entities;

namespace FeevAtend.Infrastructure.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    public DbSet<Appointment> Appointments { get; set; }
    public DbSet<User> Users { get; set; }
    public DbSet<Queue> Queues { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Configurações para Appointment
        modelBuilder.Entity<Appointment>()
            .HasIndex(a => a.RegistrationNumber)
            .IsUnique();

        modelBuilder.Entity<Appointment>()
            .Property(a => a.CreatedAt)
            .HasDefaultValueSql("CURRENT_TIMESTAMP");

        // Configurações para User
        modelBuilder.Entity<User>()
            .HasIndex(u => u.Email)
            .IsUnique();

        modelBuilder.Entity<User>()
            .Property(u => u.CreatedAt)
            .HasDefaultValueSql("CURRENT_TIMESTAMP");

        // Configurações para Queue
        modelBuilder.Entity<Queue>()
            .HasIndex(q => q.Name)
            .IsUnique();
    }
}
