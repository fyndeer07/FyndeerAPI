using FyndeerAPI.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace FyndeerAPI.Application.Common;

public interface IAppDbContext
{
    DbSet<Category> Categories { get; }
    DbSet<Professional> Professionals { get; }
    DbSet<Review> Reviews { get; }
    DbSet<TrackRecord> TrackRecords { get; }
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}
