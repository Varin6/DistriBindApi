using DistriBindApi.Models;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace DistriBindApi.Data;

using Microsoft.EntityFrameworkCore;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    public DbSet<Expense> Expenses { get; set; }
    
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Expense>()
            .HasIndex(e => e.CreatedOn)
            .HasDatabaseName("IX_Expenses_CreatedOn");

        modelBuilder.Entity<Expense>()
            .HasIndex(e => e.UserId)
            .HasDatabaseName("IX_Expenses_UserId");
    }
    
    
    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        this.SetAuditDateTimeFields();

        return base.SaveChangesAsync(cancellationToken);
    }
    
    private void SetAuditDateTimeFields()
    {
        IEnumerable<EntityEntry> addedEntities = this.ChangeTracker.Entries().Where(e => e.State == EntityState.Added && this.EntityHasProperty(e, "CreatedOn"));
        foreach (EntityEntry entry in addedEntities)
        {
            entry.Property("CreatedOn").CurrentValue = DateTime.UtcNow;
        }
        
        IEnumerable<EntityEntry> editedEntities = this.ChangeTracker.Entries().Where(e => e.State == EntityState.Modified && this.EntityHasProperty(e, "UpdatedOn"));
        foreach (EntityEntry entry in editedEntities)
        {
            entry.Property("UpdatedOn").CurrentValue = DateTime.UtcNow;
        }
        
        // Here I'd like to set the CreatedById and UpdatedById properties, but for that
        // We need to have authentication and authorization in place, hence I just use UserId for this exercise
        
    }
    
    private bool EntityHasProperty(EntityEntry entity, string propertyName)
    {
        return entity.Entity.GetType().GetProperty(propertyName) != null;
    }
}