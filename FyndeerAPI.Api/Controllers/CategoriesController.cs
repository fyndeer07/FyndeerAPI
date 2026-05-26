using FyndeerAPI.Api.Extensions;
using FyndeerAPI.Application.Categories.Commands;
using FyndeerAPI.Application.Categories.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace FyndeerAPI.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CategoriesController : ControllerBase
{
    private readonly ISender _sender;

    public CategoriesController(ISender sender)
    {
        _sender = sender;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll(CancellationToken cancellationToken)
        => (await _sender.Send(new GetAllCategoriesQuery(), cancellationToken)).ToActionResult(this);

    [HttpGet("{slug}")]
    public async Task<IActionResult> GetBySlug(string slug, CancellationToken cancellationToken)
        => (await _sender.Send(new GetCategoryBySlugQuery(slug), cancellationToken)).ToActionResult(this);

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateCategoryRequest request, CancellationToken cancellationToken)
        => (await _sender.Send(new CreateCategoryCommand(request.Slug, request.Name), cancellationToken))
            .ToCreatedResult(this, nameof(GetBySlug), new { slug = request.Slug });

    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update(int id, [FromBody] UpdateCategoryRequest request, CancellationToken cancellationToken)
        => (await _sender.Send(new UpdateCategoryCommand(id, request.Slug, request.Name), cancellationToken))
            .ToActionResult(this);

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id, CancellationToken cancellationToken)
        => (await _sender.Send(new DeleteCategoryCommand(id), cancellationToken)).ToActionResult(this);
}

public record CreateCategoryRequest(string Slug, string Name);
public record UpdateCategoryRequest(string Slug, string Name);
