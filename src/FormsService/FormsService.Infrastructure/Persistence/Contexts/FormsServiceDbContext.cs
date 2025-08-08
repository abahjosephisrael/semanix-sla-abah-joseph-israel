using FormsService.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace FormsService.Infrastructure.Persistence.Contexts;

public class FormsServiceDbContext : DbContext
{
    public FormsServiceDbContext(DbContextOptions<FormsServiceDbContext> options)
        : base(options)
    {
        ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTrackingWithIdentityResolution;
        this.Database.Migrate();
    }

    public DbSet<Form> Forms { get; set; } = default!;

    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
    {
        foreach (var entry in ChangeTracker.Entries<BaseModel>())
        {
            switch (entry.State)
            {
                case EntityState.Added:
                    entry.Entity.CreatedAt = DateTime.UtcNow;
                    entry.Entity.CreatedBy = "system"; // TODO: Set actual user
                    break;
                case EntityState.Modified:
                    entry.Entity.UpdatedAt = DateTime.UtcNow;
                    entry.Entity.UpdatedBy = "system"; // TODO: Set actual user
                    break;
            }
        }
        return base.SaveChangesAsync(cancellationToken);
    }
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSnakeCaseNamingConvention();
    }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(FormsServiceDbContext).Assembly);
        base.OnModelCreating(modelBuilder);
    }

}
