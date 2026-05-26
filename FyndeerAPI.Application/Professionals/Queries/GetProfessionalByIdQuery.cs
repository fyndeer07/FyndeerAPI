using FyndeerAPI.Application.Common;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace FyndeerAPI.Application.Professionals.Queries;

public record ReviewResponse(
    string Id,
    string ReviewerName,
    bool IsVerified,
    double Rating,
    string Comment,
    DateTime CreatedAt);

public record TrackRecordResponse(
    int HomesSold,
    int AvgDaysOnMarket,
    double SaleToListRatio,
    decimal PriceRangeMin,
    decimal PriceRangeMax,
    List<string> PrimaryCounties,
    string CommunityClients);

public record ProfessionalResponse(
    string Id,
    string Slug,
    string FullName,
    string Title,
    string CategoryId,
    string CategoryName,
    int YearsOfExperience,
    string? LicenseNumber,
    List<string> Languages,
    List<string> Specialties,
    string Phone,
    string Email,
    string? Website,
    string ServiceArea,
    string Bio,
    string? PhotoUrl,
    bool IsSponsored,
    bool IsVerified,
    double Rating,
    int ReviewCount,
    string? LicenseState,
    string? Brokerage,
    bool IsAcceptingClients,
    string? WorkingHours,
    List<ReviewResponse> Reviews,
    TrackRecordResponse? TrackRecord);

public record GetProfessionalByIdQuery(string Id) : IQuery<ProfessionalResponse>;

public class GetProfessionalByIdQueryHandler : IQueryHandler<GetProfessionalByIdQuery, ProfessionalResponse>
{
    private readonly IAppDbContext _context;

    public GetProfessionalByIdQueryHandler(IAppDbContext context)
    {
        _context = context;
    }

    public async Task<ServiceResult<ProfessionalResponse>> Handle(GetProfessionalByIdQuery request, CancellationToken cancellationToken)
    {
        var professional = await _context.Professionals
            .Include(p => p.Reviews)
            .Include(p => p.TrackRecord)
            .FirstOrDefaultAsync(p => p.Id == request.Id, cancellationToken);

        if (professional is null)
            return ServiceResult<ProfessionalResponse>.NotFound($"Professional with id '{request.Id}' was not found.");

        return ServiceResult<ProfessionalResponse>.Success(professional.ToResponse());
    }
}
