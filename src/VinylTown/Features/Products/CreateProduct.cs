using MediatR;
using Microsoft.AspNetCore.Mvc;
using VinylTown.Data;
using VinylTown.Domain;

namespace VinylTown.Features.Products;

[Route("api/Catalog")]
public class CreateProductController : Controller
{
    private readonly IMediator _mediator;
	public CreateProductController(IMediator mediator)
	{
		_mediator = mediator;
	}
	[HttpPost]
	public async Task<int> CreateProductAsync(CreateProductCommand command)
	{
		return await _mediator.Send(command);
	}

}

public record CreateProductCommand : IRequest<int>
{
    public string Name { get; init; } = string.Empty;
    public string? Description { get; init; }
    public decimal Price { get; init; }
    public string ImageUrl { get; init; } = string.Empty;
    public int ProductAuthorId { get; init; }
    public int ProductGenreId { get; init; }
}

public class CreateProductCommandHandler : IRequestHandler<CreateProductCommand, int>
{
    private readonly AppDbContext _context;
    public CreateProductCommandHandler(AppDbContext context)
    {
        _context = context;
    }

    public async Task<int> Handle(CreateProductCommand request, CancellationToken cancellationToken)
    {
        var entity = new Product();

        entity.Name = request.Name;
        entity.Description = request.Description;
        entity.Price = request.Price;
        entity.ImageUrl = request.ImageUrl;
        entity.ProductAuthorId = request.ProductAuthorId;
        entity.ProductGenreId = request.ProductGenreId;

        _context.Products.Add(entity);

        await _context.SaveChangesAsync(cancellationToken);

        return entity.Id;
    }
}