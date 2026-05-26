using FyndeerAPI.Application.Common;
using FyndeerAPI.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace FyndeerAPI.Application.Categories.Commands;

public record CreateCategoryCommand(string Slug, string Name) : ICommand<int>;

public class CreateCategoryCommandHandler : ICommandHandler<CreateCategoryCommand, int>
{
    private readonly IAppDbContext _context;

    public CreateCategoryCommandHandler(IAppDbContext context)
    {
        _context = context;
    }

    public async Task<ServiceResult<int>> Handle(CreateCategoryCommand request, CancellationToken cancellationToken)
    {
        var slugTaken = await _context.Categories
            .AnyAsync(c => c.Slug == request.Slug, cancellationToken);

        if (slugTaken)
            return ServiceResult<int>.Conflict($"A category with slug '{request.Slug}' already exists.");

        var category = Category.Create(request.Slug, request.Name);

        await _context.Categories.AddAsync(category, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);

        return ServiceResult<int>.Success(category.Id, "Category created successfully.");
    }
}
