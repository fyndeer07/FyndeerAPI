using FyndeerAPI.Application.Common;
using Microsoft.EntityFrameworkCore;

namespace FyndeerAPI.Application.Categories.Queries;

public record CategoryResponse(int Id, string Slug, string Name);

public record GetAllCategoriesQuery : IQuery<IReadOnlyList<CategoryResponse>>;

public class GetAllCategoriesQueryHandler : IQueryHandler<GetAllCategoriesQuery, IReadOnlyList<CategoryResponse>>
{
    private readonly IAppDbContext _context;

    public GetAllCategoriesQueryHandler(IAppDbContext context)
    {
        _context = context;
    }

    public async Task<ServiceResult<IReadOnlyList<CategoryResponse>>> Handle(GetAllCategoriesQuery request, CancellationToken cancellationToken)
    {
        var categories = await _context.Categories
            .AsNoTracking()
            .Select(c => new CategoryResponse(c.Id, c.Slug, c.Name))
            .ToListAsync(cancellationToken);

        return ServiceResult<IReadOnlyList<CategoryResponse>>.Success(categories);
    }
}
