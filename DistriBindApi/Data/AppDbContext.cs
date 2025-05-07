using DistriBindApi.Models;

namespace DistriBindApi.Data;

using Microsoft.EntityFrameworkCore;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    public DbSet<Expense> Expenses { get; set; }
    public DbSet<Category> Categories { get; set; }
    
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Expense>()
            .HasIndex(e => e.Timestamp)
            .HasDatabaseName("IX_Expenses_Timestamp");

        modelBuilder.Entity<Expense>()
            .HasIndex(e => e.UserId)
            .HasDatabaseName("IX_Expenses_UserId");
    }
}