using VinylTown.Domain;

namespace VinylTown.Data;

public static class AppDbContextSeed
{
    public static async Task EnsureSeedData(WebApplication serviceScope)
    {
        var services = serviceScope.Services.CreateScope().ServiceProvider;
        await EnsureSeedData(services);
    }

    public static async Task EnsureSeedData(IServiceProvider serviceProvider)
    {
        using var scope = serviceProvider.GetRequiredService<IServiceScopeFactory>().CreateScope();
        var env = scope.ServiceProvider.GetRequiredService<IWebHostEnvironment>();

        var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();

        if (env.IsDevelopment() || env.IsEnvironment("Docker"))
        {
            await context.Database.EnsureCreatedAsync();
            await EnsureSeedCatalog(context);
        }
    }

    private static async Task EnsureSeedCatalog(AppDbContext context)
    {
        if (!context.ProductAuthors.Any())
        {
            await context.ProductAuthors.AddAsync(new ProductAuthor() { Author = "$uicideboy$" });
            await context.ProductAuthors.AddAsync(new ProductAuthor() { Author = "Metallica" });
            await context.ProductAuthors.AddAsync(new ProductAuthor() { Author = "System Of A Down" });
        }

        if (!context.ProductGenres.Any())
        {
            await context.ProductGenres.AddAsync(new ProductGenre() { Genre = "Hip-hop" });
            await context.ProductGenres.AddAsync(new ProductGenre() { Genre = "Metal" });
        }

        if (!context.Products.Any())
        {
            await context.Products.AddAsync(new Product() { Name = "I Want To Die In New Orleans Vinyl LP", Description = "text", Price = 22, Image = "suicideboysIWTDINO.webp", Stock = 5, ProductAuthorId = 1, ProductGenreId = 1 });
            await context.Products.AddAsync(new Product() { Name = "Sing Me A Lullaby, My Sweet Temptation Vinyl LP", Description = "text", Price = 24.95M, Image = "suicideboysMST.webp", Stock = 5, ProductAuthorId = 1, ProductGenreId = 1 });
            await context.Products.AddAsync(new Product() { Name = "Ride The Lightning Vinyl LP", Description = "text", Price = 24.95M, Image = "metallicaRDL.webp", Stock = 15, ProductAuthorId = 2, ProductGenreId = 2 });
            await context.Products.AddAsync(new Product() { Name = "Master Of Puppets Vinyl LP", Description = "text", Price = 24.95M, Image = "metallicaMOP.webp", Stock = 15, ProductAuthorId = 2, ProductGenreId = 2 });
            await context.Products.AddAsync(new Product() { Name = "Metallica Vinyl 2LP", Description = "text", Price = 24.95M, Image = "metallicaMetallica.webp", Stock = 15, ProductAuthorId = 2, ProductGenreId = 2 });
            await context.Products.AddAsync(new Product() { Name = "System Of A Down Vinyl LP", Description = "text", Price = 19.95M, Image = "soadSOAD.webp", Stock = 15, ProductAuthorId = 3, ProductGenreId = 2 });
            await context.Products.AddAsync(new Product() { Name = "Toxicity Vinyl LP", Description = "text", Price = 21.95M, Image = "soadToxicity.webp", Stock = 15, ProductAuthorId = 3, ProductGenreId = 2 });
            await context.Products.AddAsync(new Product() { Name = "Steal This Album! Vinyl 2LP", Description = "text", Price = 24.95M, Image = "soadSTA.webp", Stock = 15, ProductAuthorId = 3, ProductGenreId = 2 });
            await context.Products.AddAsync(new Product() { Name = "Mezmerize Vinyl LP", Description = "text", Price = 19.95M, Image = "soadMezmerize.webp", Stock = 15, ProductAuthorId = 3, ProductGenreId = 2 });
            await context.Products.AddAsync(new Product() { Name = "Hypnotize Vinyl LP", Description = "text", Price = 19.95M, Image = "soadHypnotize.webp", Stock = 15, ProductAuthorId = 3, ProductGenreId = 2 });

            //await context.Products.AddAsync(new Product() { Name = "", Description = "", Price = , Image = "", Stock = , ProductAuthorId = , ProductGenreId =  });
        }
        await context.SaveChangesAsync();
    }
}
