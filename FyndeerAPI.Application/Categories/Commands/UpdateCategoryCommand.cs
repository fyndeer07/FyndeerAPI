using FyndeerAPI.Application.Common;
using Microsoft.EntityFrameworkCore;

namespace FyndeerAPI.Application.Categories.Commands;

public record UpdateCategoryCommand(int Id, string Slug, string Name) : ICommand;

public class UpdateCategoryCommandHandler : ICommandHandler<UpdateCategoryCommand>
{
    private readonly IAppDbContext _context;

    public UpdateCategoryCommandHandler(IAppDbContext context)
    {
        _context = context;
    }

    public async Task<ServiceResult> Handle(UpdateCategoryCommand request, CancellationToken cancellationToken)
    {
        var category = await _context.Categories
            .FindAsync([request.Id], cancellationToken);

        if (category is null)
            return ServiceResult.NotFound($"Category with id {request.Id} was not found.");

        var slugTaken = await _context.Categories
            .AnyAsync(c => c.Slug == request.Slug && c.Id != request.Id, cancellationToken);

        if (slugTaken)
            return ServiceResult.Conflict($"A category with slug '{request.Slug}' already exists.");

        category.Update(request.Slug, request.Name);
        await _context.SaveChangesAsync(cancellationToken);

        return ServiceResult.Success("Category updated successfully.");
    }
}
