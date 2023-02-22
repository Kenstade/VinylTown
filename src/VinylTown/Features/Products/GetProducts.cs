using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using VinylTown.Data;

namespace VinylTown.Features.Products;

[Route("api/Catalog")]
public class GetProductsController : Controller
{
    private readonly IMediator _mediator;
    public GetProductsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<object> GetProductsAsync()
    {
        var viewModel = await _mediator.Send(new GetProductsQuery());

        return viewModel;
    }
}

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
    public string ImageUrl { get; set; } = string.Empty;
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
            .Select(p => new ProductSummaryViewModel
            {
                Id = p.Id,
                Name = p.Name,
                Price = p.Price,
                ImageUrl = p.ImageUrl,
                ProductAuthorId = p.ProductAuthorId,
                ProductGenreId = p.ProductGenreId
            }).ToArrayAsync(cancellationToken);

        return result;
    }

}
