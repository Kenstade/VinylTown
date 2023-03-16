using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using VinylTown.Data;

namespace VinylTown.Features.Products;

public record GetProductsQuery : IRequest<ProductsViewModel>;

public class ProductsViewModel
{
    public IEnumerable<ProductSummaryViewModel> Products { get; set; } = null!;
}

public class ProductSummaryViewModel
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public decimal Price { get; set; }
    public string Image { get; set; } = string.Empty;
    public int Stock { get; set; }
    public int ProductAuthorId { get; set; }
    public int ProductGenreId { get; set; }
}

public class GetProductsQueryHandler : IRequestHandler<GetProductsQuery, ProductsViewModel>
{
    private readonly AppDbContext _context;
    public GetProductsQueryHandler(AppDbContext context)
    {
        _context = context;
    }

    public async Task<ProductsViewModel> Handle(GetProductsQuery request, CancellationToken cancellationToken)
    {
        var result = new ProductsViewModel();
        result.Products = await _context.Products
            .AsNoTracking()
            .Select(p => new ProductSummaryViewModel
            {
                Id = p.Id,
                Name = p.Name,
                Price = p.Price,
                Image = p.Image,
                Stock = p.Stock,
                ProductAuthorId = p.ProductAuthorId,
                ProductGenreId = p.ProductGenreId
            }).ToArrayAsync(cancellationToken);

        return result;
    }

}
