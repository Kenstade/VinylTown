using Microsoft.EntityFrameworkCore;
using VinylTown.Domain;

namespace VinylTown.Data;

public class AppDbContextSeed
{
    private readonly AppDbContext _context;
    private readonly ILogger<AppDbContext> _logger;

    public AppDbContextSeed(AppDbContext context, ILogger<AppDbContext> logger)
    {
        _context = context;
        _logger = logger;
    }
    public async Task SeedAsync()
    {
        try
        {
            if (_context.Database.IsSqlServer())
            {
                await _context.Database.MigrateAsync();
            }

            await TrySeedAsync();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occured while seeding the db");
            throw;
        }
    }
    public async Task TrySeedAsync()
    {
        if (!_context.ProductGenres.Any())
        {
            await _context.ProductGenres.AddRangeAsync(new List<ProductGenre>
            {
                new ProductGenre
                {
                    Genre = "Hip-hop",
                },
                new ProductGenre
                {
                    Genre = "Alternative Hip-hop"
                }
            });
        }
        if (!_context.ProductAuthors.Any())
        {
            await _context.ProductAuthors.AddRangeAsync(new List<ProductAuthor>
            {
                new ProductAuthor
                {
                    Author = "Metro Boomin"
                },
                new ProductAuthor
                {
                    Author = "Kid Cudi"
                }
            });
        }
        if (!_context.Products.Any())
        {
            await _context.Products.AddRangeAsync(new List<Product>
            {
                new Product
                {
                    Name = "Heroes & Villains",
                    Description = "Some text",
                    Price = 28,
                    ImageUrl = "beb",
                    ProductAuthorId = 1,
                    ProductGenreId = 1
                },
                new Product
                {
                    Name = "Man on the Moon II: The Legend of Mr. Rager",
                    Description = "Some text",
                    Price = 30,
                    ImageUrl = "beb",
                    ProductAuthorId = 2,
                    ProductGenreId = 2
                }
            });
        }

        await _context.SaveChangesAsync();
    }
}
