using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using VinylTown.Data;

namespace VinylTown.Features.Products;

public record GetProductDetailsQuery : IRequest<ProductDetailsViewModel>
{
    public int Id { get; init; }
}

public class ProductDetailsViewModel
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string? Description { get; set; }
    public decimal Price { get; set; }
    public string Image { get; set; } = string.Empty;
    public int Stock { get; set; }
    public int ProductAuthorId { get; set; }
    public int ProductGenreId { get; set; }
}

public class GetProductDetailsQueryHandler : IRequestHandler<GetProductDetailsQuery, ProductDetailsViewModel>
{
    private readonly AppDbContext _context;
    public GetProductDetailsQueryHandler(AppDbContext context)
    {
        _context = context;
    }

    public async Task<ProductDetailsViewModel> Handle(GetProductDetailsQuery request, CancellationToken cancellationToken)
    {
        var product = await _context.Products
            .AsNoTracking()
            .FirstOrDefaultAsync(p => p.Id == request.Id);

        if (product == null)
        {
            return null;
        }

        return new ProductDetailsViewModel
        {
            Id = product.Id,
            Name = product.Name,
            Description = product.Description,
            Price = product.Price,
            Image = product.Image,
            Stock = product.Stock,
            ProductAuthorId = product.ProductAuthorId,
            ProductGenreId = product.ProductGenreId
        };
    }
}
