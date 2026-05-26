using FyndeerAPI.Domain.Entities;

namespace FyndeerAPI.Application.Professionals.Queries;

internal static class ProfessionalMappingExtensions
{
    internal static ProfessionalResponse ToResponse(this Professional p) => new(
        p.Id,
        p.Slug,
        p.FullName,
        p.Title,
        p.CategoryId,
        p.CategoryName,
        p.YearsOfExperience,
        p.LicenseNumber,
        p.Languages,
        p.Specialties,
        p.Phone,
        p.Email,
        p.Website,
        p.ServiceArea,
        p.Bio,
        p.PhotoUrl,
        p.IsSponsored,
        p.IsVerified,
        p.Rating,
        p.ReviewCount,
        p.LicenseState,
        p.Brokerage,
        p.IsAcceptingClients,
        p.WorkingHours,
        p.Reviews.Select(r => new ReviewResponse(
            r.Id, r.ReviewerName, r.IsVerified, r.Rating, r.Comment, r.CreatedAt)).ToList(),
        p.TrackRecord is null ? null : new TrackRecordResponse(
            p.TrackRecord.HomesSold,
            p.TrackRecord.AvgDaysOnMarket,
            p.TrackRecord.SaleToListRatio,
            p.TrackRecord.PriceRangeMin,
            p.TrackRecord.PriceRangeMax,
            p.TrackRecord.PrimaryCounties,
            p.TrackRecord.CommunityClients));
}
