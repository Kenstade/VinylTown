using Microsoft.AspNetCore.Identity;
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

        var userManager = scope.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();
        var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();

        var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();

        if (env.IsDevelopment() || env.IsEnvironment("Docker"))
        {
            await context.Database.EnsureCreatedAsync();
            await EnsureSeedCatalog(context);
            await EnsureSeedIdentity(userManager, roleManager);
        }
    }

    private static async Task EnsureSeedCatalog(AppDbContext context)
    {
        if (!context.ProductAuthors.Any())
        {
            await context.ProductAuthors.AddAsync(new ProductAuthor() { Author = "Metallica" });
            await context.ProductAuthors.AddAsync(new ProductAuthor() { Author = "Megadeth" });
        }

        if (!context.ProductGenres.Any())
        {
            await context.ProductGenres.AddAsync(new ProductGenre() { Genre = "Metal" });
        }

        if (!context.Products.Any())
        {
            await context.Products.AddAsync(new Product() { Name = "Kill 'Em All Vinyl LP", Description = "text", Price = 19.95M, Image = "metallica-KEA.webp", Stock = 15, ProductAuthorId = 1, ProductGenreId = 1 });
            await context.Products.AddAsync(new Product() { Name = "Ride The Lightning Vinyl LP", Description = "text", Price = 24.95M, Image = "metallica-RTL.webp", Stock = 15, ProductAuthorId = 1, ProductGenreId = 1 });
            await context.Products.AddAsync(new Product() { Name = "Master Of Puppets Vinyl LP", Description = "text", Price = 24.95M, Image = "metallica-MOP.webp", Stock = 15, ProductAuthorId = 1, ProductGenreId = 1 });
            await context.Products.AddAsync(new Product() { Name = "And Justice For All Vinyl 2LP", Description = "text", Price = 29.95M, Image = "metallica-AJFA.webp", Stock = 15, ProductAuthorId = 1, ProductGenreId = 1 });
            await context.Products.AddAsync(new Product() { Name = "Load Vinyl 2LP", Description = "text", Price = 29.95M, Image = "metallica-Reload.webp", Stock = 15, ProductAuthorId = 1, ProductGenreId = 1 });
            await context.Products.AddAsync(new Product() { Name = "Reload Vinyl 2LP", Description = "text", Price = 29.95M, Image = "metallica-Load.webp", Stock = 15, ProductAuthorId = 1, ProductGenreId = 1 });
            await context.Products.AddAsync(new Product() { Name = "Garage Inc. Vinyl 3LP", Description = "text", Price = 34.95M, Image = "metallica-GI.webp", Stock = 15, ProductAuthorId = 1, ProductGenreId = 1 });
            await context.Products.AddAsync(new Product() { Name = "St. Anger Vinyl 2LP", Description = "text", Price = 29.95M, Image = "metallica-SA.webp", Stock = 15, ProductAuthorId = 1, ProductGenreId = 1 });
            await context.Products.AddAsync(new Product() { Name = "Death Magnetic Vinyl 2LP", Description = "text", Price = 29.95M, Image = "metallica-DM.webp", Stock = 15, ProductAuthorId = 1, ProductGenreId = 1 });
            await context.Products.AddAsync(new Product() { Name = "Hardwired...To Self-Destruct Vinyl 2LP", Description = "text", Price = 29.95M, Image = "metallica-HTSD.webp", Stock = 15, ProductAuthorId = 1, ProductGenreId = 1 });
            await context.Products.AddAsync(new Product() { Name = "72 Seasons Vinyl 2LP", Description = "text", Price = 34.95M, Image = "metallica-72S.webp", Stock = 15, ProductAuthorId = 1, ProductGenreId = 1 });
            await context.Products.AddAsync(new Product() { Name = "Peace Sells… But Who's Buying? Vinyl 1LP", Description = "text", Price = 24.95M, Image = "megadeth-PSBWB.webp", Stock = 15, ProductAuthorId = 2, ProductGenreId = 1 });
            await context.Products.AddAsync(new Product() { Name = "Rust In Peace Vinyl 1LP", Description = "text", Price = 29.95M, Image = "megadeth-RIP.webp", Stock = 15, ProductAuthorId = 2, ProductGenreId = 1 });
            await context.Products.AddAsync(new Product() { Name = "The Sick, The Dying... And The Dead! Vinyl 2LP", Description = "text", Price = 34.95M, Image = "megadeth-TSTDATD.webp", Stock = 15, ProductAuthorId = 2, ProductGenreId = 1 });

            //await context.Products.AddAsync(new Product() { Name = "", Description = "text", Price = , Image = "", Stock = , ProductAuthorId = , ProductGenreId =  });
        }
        await context.SaveChangesAsync();
    }

    private static async Task EnsureSeedIdentity(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
    {
        await roleManager.CreateAsync(new IdentityRole("Admin"));

        var defaultUser = new ApplicationUser { UserName = "user@mail.com", Email = "user@mail.com" };
        await userManager.CreateAsync(defaultUser, password:"Password@1");

        var adminUser = new ApplicationUser { UserName = "admin@mail.com", Email = "admin@mail.com" };
        await userManager.CreateAsync(adminUser, password:"Password@1");
        await userManager.AddToRoleAsync(adminUser, "Admin");
    }
}
