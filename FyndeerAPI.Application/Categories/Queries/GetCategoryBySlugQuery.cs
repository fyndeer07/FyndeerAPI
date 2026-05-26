using FyndeerAPI.Application.Common;
using Microsoft.EntityFrameworkCore;

namespace FyndeerAPI.Application.Categories.Queries;

public record GetCategoryBySlugQuery(string Slug) : IQuery<CategoryResponse>;

public class GetCategoryBySlugQueryHandler : IQueryHandler<GetCategoryBySlugQuery, CategoryResponse>
{
    private readonly IAppDbContext _context;

    public GetCategoryBySlugQueryHandler(IAppDbContext context)
    {
        _context = context;
    }

    public async Task<ServiceResult<CategoryResponse>> Handle(GetCategoryBySlugQuery request, CancellationToken cancellationToken)
    {
        var category = await _context.Categories
            .AsNoTracking()
            .FirstOrDefaultAsync(c => c.Slug == request.Slug, cancellationToken);

        if (category is null)
            return ServiceResult<CategoryResponse>.NotFound($"Category with slug '{request.Slug}' was not found.");

        return ServiceResult<CategoryResponse>.Success(new CategoryResponse(category.Id, category.Slug, category.Name));
    }
}
