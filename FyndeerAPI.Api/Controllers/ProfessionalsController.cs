using FyndeerAPI.Api.Extensions;
using FyndeerAPI.Application.Professionals.Commands;
using FyndeerAPI.Application.Professionals.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace FyndeerAPI.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProfessionalsController : ControllerBase
{
    private readonly ISender _sender;

    public ProfessionalsController(ISender sender) => _sender = sender;

    [HttpGet]
    public async Task<IActionResult> GetAll(CancellationToken cancellationToken)
        => (await _sender.Send(new GetAllProfessionalsQuery(), cancellationToken)).ToActionResult(this);

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(string id, CancellationToken cancellationToken)
        => (await _sender.Send(new GetProfessionalByIdQuery(id), cancellationToken)).ToActionResult(this);

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateProfessionalRequest request, CancellationToken cancellationToken)
        => (await _sender.Send(new CreateProfessionalCommand(
                request.Id, request.Slug, request.FullName, request.Title,
                request.CategoryId, request.CategoryName, request.YearsOfExperience,
                request.LicenseNumber, request.Languages, request.Specialties,
                request.Phone, request.Email, request.Website, request.ServiceArea,
                request.Bio, request.PhotoUrl, request.IsSponsored, request.IsVerified,
                request.Rating, request.ReviewCount, request.LicenseState,
                request.Brokerage, request.IsAcceptingClients, request.WorkingHours), cancellationToken))
            .ToCreatedResult(this, nameof(GetById), new { id = request.Id });

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(string id, [FromBody] UpdateProfessionalRequest request, CancellationToken cancellationToken)
        => (await _sender.Send(new UpdateProfessionalCommand(
                id, request.Slug, request.FullName, request.Title,
                request.CategoryId, request.CategoryName, request.YearsOfExperience,
                request.LicenseNumber, request.Languages, request.Specialties,
                request.Phone, request.Email, request.Website, request.ServiceArea,
                request.Bio, request.PhotoUrl, request.IsSponsored, request.IsVerified,
                request.LicenseState, request.Brokerage, request.IsAcceptingClients, request.WorkingHours), cancellationToken))
            .ToActionResult(this);

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(string id, CancellationToken cancellationToken)
        => (await _sender.Send(new DeleteProfessionalCommand(id), cancellationToken)).ToActionResult(this);
}

public record CreateProfessionalRequest(
    string Id, string Slug, string FullName, string Title,
    string CategoryId, string CategoryName, int YearsOfExperience,
    string? LicenseNumber, List<string> Languages, List<string> Specialties,
    string Phone, string Email, string? Website, string ServiceArea,
    string Bio, string? PhotoUrl, bool IsSponsored, bool IsVerified,
    double Rating, int ReviewCount, string? LicenseState,
    string? Brokerage, bool IsAcceptingClients, string? WorkingHours);

public record UpdateProfessionalRequest(
    string Slug, string FullName, string Title,
    string CategoryId, string CategoryName, int YearsOfExperience,
    string? LicenseNumber, List<string> Languages, List<string> Specialties,
    string Phone, string Email, string? Website, string ServiceArea,
    string Bio, string? PhotoUrl, bool IsSponsored, bool IsVerified,
    string? LicenseState, string? Brokerage, bool IsAcceptingClients, string? WorkingHours);
