using FyndeerAPI.Application.Common;
using FyndeerAPI.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace FyndeerAPI.Application.Professionals.Commands;

public record CreateProfessionalCommand(
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
    string? WorkingHours) : ICommand<string>;

public class CreateProfessionalCommandHandler : ICommandHandler<CreateProfessionalCommand, string>
{
    private readonly IAppDbContext _context;

    public CreateProfessionalCommandHandler(IAppDbContext context)
    {
        _context = context;
    }

    public async Task<ServiceResult<string>> Handle(CreateProfessionalCommand request, CancellationToken cancellationToken)
    {
        var slugTaken = await _context.Professionals
            .AnyAsync(p => p.Slug == request.Slug, cancellationToken);

        if (slugTaken)
            return ServiceResult<string>.Conflict($"A professional with slug '{request.Slug}' already exists.");

        var emailTaken = await _context.Professionals
            .AnyAsync(p => p.Email == request.Email, cancellationToken);

        if (emailTaken)
            return ServiceResult<string>.Conflict($"A professional with email '{request.Email}' already exists.");

        var professional = Professional.Create(
            request.Id,
            request.Slug,
            request.FullName,
            request.Title,
            request.CategoryId,
            request.CategoryName,
            request.YearsOfExperience,
            request.LicenseNumber,
            request.Languages,
            request.Specialties,
            request.Phone,
            request.Email,
            request.Website,
            request.ServiceArea,
            request.Bio,
            request.PhotoUrl,
            request.IsSponsored,
            request.IsVerified,
            request.Rating,
            request.ReviewCount,
            request.LicenseState,
            request.Brokerage,
            request.IsAcceptingClients,
            request.WorkingHours);

        await _context.Professionals.AddAsync(professional, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);

        return ServiceResult<string>.Success(professional.Id, "Professional created successfully.");
    }
}
