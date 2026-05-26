using FyndeerAPI.Application.Common;

namespace FyndeerAPI.Application.Categories.Commands;

public record DeleteCategoryCommand(int Id) : ICommand;

public class DeleteCategoryCommandHandler : ICommandHandler<DeleteCategoryCommand>
{
    private readonly IAppDbContext _context;

    public DeleteCategoryCommandHandler(IAppDbContext context)
    {
        _context = context;
    }

    public async Task<ServiceResult> Handle(DeleteCategoryCommand request, CancellationToken cancellationToken)
    {
        var category = await _context.Categories
            .FindAsync([request.Id], cancellationToken);

        if (category is null)
            return ServiceResult.NotFound($"Category with id {request.Id} was not found.");

        _context.Categories.Remove(category);
        await _context.SaveChangesAsync(cancellationToken);

        return ServiceResult.Success("Category deleted successfully.");
    }
}
