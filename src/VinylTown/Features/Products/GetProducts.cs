using MediatR;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using VinylTown.Data;
using VinylTown.Domain;
using VinylTown.ViewModels;

namespace VinylTown.Features.Products;

public record GetProductsQuery : IRequest<ProductsViewModel>
{
    public int PageNumber { get; init; }
    public int PageSize { get; init; } = 12;
    public string Search { get; init; } = "";
    public int? AuthorId { get; init; }
}

public class ProductsViewModel
{
    public IEnumerable<ProductSummaryViewModel> Products { get; set; } = null!;
    public SelectList ProductAuthors { get; set; } = null!;
    public PagingInfo PagingInfo { get; set;} = null!;
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
        .Include(p => p.ProductAuthor)
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
        })
        .Where(p =>!request.AuthorId.HasValue || p.ProductAuthorId == request.AuthorId)
        .Where(p => EF.Functions.Like(p.Name, $"%{request.Search}%"))
        .Skip((request.PageNumber - 1) * request.PageSize)
        .Take(request.PageSize)
        .ToArrayAsync(cancellationToken);

        

        var total = await _context.Products.AsNoTracking().Where(p => EF.Functions.Like(p.Name, $"%{request.Search}%")).CountAsync();

        result.PagingInfo = new PagingInfo()
        {
            PageNumber = request.PageNumber,
            TotalItems = total,
            TotalPages = (int)Math.Ceiling((decimal)total / request.PageSize)
        };
            
        return result;
    }

}
