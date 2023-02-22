using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using VinylTown.Data;

namespace VinylTown.Features.Products;

[Route("api/Catalog")]
public class GetProductDetailsController : Controller
{
    private readonly IMediator _mediator;
    public GetProductDetailsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetProductDetailsAsync(int id)
    {
        if (id <= 0)
        {
            return BadRequest();
        }

        var viewModel = await _mediator.Send(new GetProductDetailsQuery { Id = id});

        if (viewModel == null)
        {
            return BadRequest("afesdf");
        }
        return Ok(viewModel);
    }
}

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
    public string ImageUrl { get; set; } = string.Empty;
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
        var product = await _context.Products.FirstOrDefaultAsync(p => p.Id == request.Id);

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
            ImageUrl = product.ImageUrl,
            ProductAuthorId = product.ProductAuthorId,
            ProductGenreId = product.ProductGenreId
        };
    }
}
