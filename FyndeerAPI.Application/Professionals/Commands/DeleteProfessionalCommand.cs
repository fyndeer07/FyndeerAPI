using FyndeerAPI.Application.Common;

namespace FyndeerAPI.Application.Professionals.Commands;

public record DeleteProfessionalCommand(string Id) : ICommand;

public class DeleteProfessionalCommandHandler : ICommandHandler<DeleteProfessionalCommand>
{
    private readonly IAppDbContext _context;

    public DeleteProfessionalCommandHandler(IAppDbContext context)
    {
        _context = context;
    }

    public async Task<ServiceResult> Handle(DeleteProfessionalCommand request, CancellationToken cancellationToken)
    {
        var professional = await _context.Professionals
            .FindAsync([request.Id], cancellationToken);

        if (professional is null)
            return ServiceResult.NotFound($"Professional with id '{request.Id}' was not found.");

        _context.Professionals.Remove(professional);
        await _context.SaveChangesAsync(cancellationToken);

        return ServiceResult.Success("Professional deleted successfully.");
    }
}
