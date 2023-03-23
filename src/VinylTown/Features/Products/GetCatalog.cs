using MediatR;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using VinylTown.Data;

namespace VinylTown.Features.Products;

public record GetCatalogQuery : IRequest<CatalogViewModel>;

public class CatalogViewModel
{
    public IEnumerable<ProductSummaryViewModel> Products { get; set; } = null!;
    public IEnumerable<SelectListItem> Authors { get; set; } = null!;
    public IEnumerable<SelectListItem> Genres { get; set; } = null!;
}

public class GetCatalogQueryHandler : IRequestHandler<GetCatalogQuery, CatalogViewModel>
{
    private readonly AppDbContext _context;
    public GetCatalogQueryHandler(AppDbContext context)
    {
        _context = context;
    }

    public async Task<CatalogViewModel> Handle(GetCatalogQuery request, CancellationToken cancellationToken)
    {
        var result = new CatalogViewModel();
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
            })
            .ToArrayAsync(cancellationToken);

         result.Authors = await _context.ProductAuthors
            .AsNoTracking()
            .Select(author => new SelectListItem() { Value = author.Id.ToString(), Text = author.Author})
            .OrderBy(a => a.Text)
            .ToArrayAsync(cancellationToken);

        result.Genres = await _context.ProductGenres
            .AsNoTracking()
            .Select(genre => new SelectListItem() { Value = genre.Id.ToString(), Text = genre.Genre })
            .OrderBy(a => a.Text)
            .ToArrayAsync(cancellationToken);

        return result;
    }
}

