using FyndeerAPI.Application.Common;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace FyndeerAPI.Application.Professionals.Queries;

public record GetAllProfessionalsQuery : IQuery<IReadOnlyList<ProfessionalResponse>>;

public class GetAllProfessionalsQueryHandler : IQueryHandler<GetAllProfessionalsQuery, IReadOnlyList<ProfessionalResponse>>
{
    private readonly IAppDbContext _context;

    public GetAllProfessionalsQueryHandler(IAppDbContext context)
    {
        _context = context;
    }

    public async Task<ServiceResult<IReadOnlyList<ProfessionalResponse>>> Handle(GetAllProfessionalsQuery request, CancellationToken cancellationToken)
    {
        var professionals = await _context.Professionals
            .Include(p => p.Reviews)
            .Include(p => p.TrackRecord)
            .AsNoTracking()
            .ToListAsync(cancellationToken);

        return ServiceResult<IReadOnlyList<ProfessionalResponse>>.Success(
            professionals.Select(p => p.ToResponse()).ToList());
    }
}
