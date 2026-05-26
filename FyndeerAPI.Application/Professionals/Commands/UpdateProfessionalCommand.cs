using FyndeerAPI.Application.Common;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace FyndeerAPI.Application.Professionals.Commands;

public record UpdateProfessionalCommand(
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
    string? LicenseState,
    string? Brokerage,
    bool IsAcceptingClients,
    string? WorkingHours) : ICommand;

public class UpdateProfessionalCommandHandler : ICommandHandler<UpdateProfessionalCommand>
{
    private readonly IAppDbContext _context;

    public UpdateProfessionalCommandHandler(IAppDbContext context)
    {
        _context = context;
    }

    public async Task<ServiceResult> Handle(UpdateProfessionalCommand request, CancellationToken cancellationToken)
    {
        var professional = await _context.Professionals
            .FindAsync([request.Id], cancellationToken);

        if (professional is null)
            return ServiceResult.NotFound($"Professional with id '{request.Id}' was not found.");

        var slugTaken = await _context.Professionals
            .AnyAsync(p => p.Slug == request.Slug && p.Id != request.Id, cancellationToken);

        if (slugTaken)
            return ServiceResult.Conflict($"A professional with slug '{request.Slug}' already exists.");

        var emailTaken = await _context.Professionals
            .AnyAsync(p => p.Email == request.Email && p.Id != request.Id, cancellationToken);

        if (emailTaken)
            return ServiceResult.Conflict($"A professional with email '{request.Email}' already exists.");

        professional.Update(
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
            request.LicenseState,
            request.Brokerage,
            request.IsAcceptingClients,
            request.WorkingHours);

        await _context.SaveChangesAsync(cancellationToken);

        return ServiceResult.Success("Professional updated successfully.");
    }
}
