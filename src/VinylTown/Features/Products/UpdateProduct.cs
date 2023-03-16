using MediatR;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using VinylTown.Data;

namespace VinylTown.Features.Products;

public record UpdateProductCommand : IRequest
{
	public int Id { get; init; }
	public string Name { get; init; } = string.Empty;
	public string? Description { get; init; }
	public decimal Price { get; init; }
	public string Image { get; init; } = string.Empty;
	public int Stock { get; init; }
	public int ProductAuthorId { get; init; }
	public int ProductGenreId { get; init; }
}

public class UpdateProductCommandHandler : IRequestHandler<UpdateProductCommand>
{
	private readonly AppDbContext _context;
	public UpdateProductCommandHandler(AppDbContext context)
	{
		_context = context;
	}
    public async Task<Unit> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
    {
        var product = await _context.Products.SingleOrDefaultAsync(p => p.Id == request.Id, cancellationToken);

		if (product == null)
		{
            throw new Exception();
        }

		product.Name = request.Name;
		product.Description = request.Description;
		product.Price = request.Price;
		product.Image = request.Image;
		product.Stock = request.Stock;
		product.ProductAuthorId = request.ProductAuthorId;
		product.ProductGenreId = request.ProductGenreId;

		await _context.SaveChangesAsync();

		return Unit.Value;
    }
}
