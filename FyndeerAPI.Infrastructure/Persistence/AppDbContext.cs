using FyndeerAPI.Application.Common;
using FyndeerAPI.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace FyndeerAPI.Infrastructure.Persistence;

public class AppDbContext : DbContext, IAppDbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    public DbSet<Category> Categories => Set<Category>();
    public DbSet<Professional> Professionals => Set<Professional>();
    public DbSet<Review> Reviews => Set<Review>();
    public DbSet<TrackRecord> TrackRecords => Set<TrackRecord>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);
        base.OnModelCreating(modelBuilder);
    }
}
