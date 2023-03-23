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
            await context.ProductAuthors.AddAsync(new ProductAuthor() { Author = "$uicideboy$" });
            await context.ProductAuthors.AddAsync(new ProductAuthor() { Author = "Metallica" });
            await context.ProductAuthors.AddAsync(new ProductAuthor() { Author = "System Of A Down" });
            await context.ProductAuthors.AddAsync(new ProductAuthor() { Author = "Nas" });
            await context.ProductAuthors.AddAsync(new ProductAuthor() { Author = "MF DOOM" });
            await context.ProductAuthors.AddAsync(new ProductAuthor() { Author = "Gorillaz" });
            await context.ProductAuthors.AddAsync(new ProductAuthor() { Author = "Arctic Monkeys" });
            await context.ProductAuthors.AddAsync(new ProductAuthor() { Author = "Kid Cudi" });
        }

        if (!context.ProductGenres.Any())
        {
            await context.ProductGenres.AddAsync(new ProductGenre() { Genre = "Hip-hop" });
            await context.ProductGenres.AddAsync(new ProductGenre() { Genre = "Metal" });
            await context.ProductGenres.AddAsync(new ProductGenre() { Genre = "Alternative" });
            await context.ProductGenres.AddAsync(new ProductGenre() { Genre = "Rock / Indie" });
        }

        if (!context.Products.Any())
        {
            await context.Products.AddAsync(new Product() { Name = "Kill 'Em All Vinyl LP", Description = "text", Price = 19.95M, Image = "metallicaKEA.webp", Stock = 15, ProductAuthorId = 2, ProductGenreId = 2 });
            await context.Products.AddAsync(new Product() { Name = "Ride The Lightning Vinyl LP", Description = "text", Price = 24.95M, Image = "metallicaRDL.webp", Stock = 15, ProductAuthorId = 2, ProductGenreId = 2 });
            await context.Products.AddAsync(new Product() { Name = "Master Of Puppets Vinyl LP", Description = "text", Price = 24.95M, Image = "metallicaMOP.webp", Stock = 15, ProductAuthorId = 2, ProductGenreId = 2 });
            await context.Products.AddAsync(new Product() { Name = "And Justice For All Vinyl 2LP", Description = "text", Price = 29.95M, Image = "metallicaAJFAL.webp", Stock = 15, ProductAuthorId = 2, ProductGenreId = 2 });
            await context.Products.AddAsync(new Product() { Name = "Metallica Vinyl 2LP", Description = "text", Price = 24.95M, Image = "metallicaMetallica.webp", Stock = 15, ProductAuthorId = 2, ProductGenreId = 2 });
            await context.Products.AddAsync(new Product() { Name = "Garage Inc. Vinyl 3LP", Description = "text", Price = 34.95M, Image = "metallicaGarageInc.webp", Stock = 15, ProductAuthorId = 2, ProductGenreId = 2 });
            await context.Products.AddAsync(new Product() { Name = "System Of A Down Vinyl LP", Description = "text", Price = 19.95M, Image = "soadSOAD.webp", Stock = 15, ProductAuthorId = 3, ProductGenreId = 2 });
            await context.Products.AddAsync(new Product() { Name = "Toxicity Vinyl LP", Description = "text", Price = 21.95M, Image = "soadToxicity.webp", Stock = 15, ProductAuthorId = 3, ProductGenreId = 2 });
            await context.Products.AddAsync(new Product() { Name = "Steal This Album! Vinyl 2LP", Description = "text", Price = 24.95M, Image = "soadSTA.webp", Stock = 15, ProductAuthorId = 3, ProductGenreId = 2 });
            await context.Products.AddAsync(new Product() { Name = "Mezmerize Vinyl LP", Description = "text", Price = 19.95M, Image = "soadMezmerize.webp", Stock = 15, ProductAuthorId = 3, ProductGenreId = 2 });
            await context.Products.AddAsync(new Product() { Name = "Hypnotize Vinyl LP", Description = "text", Price = 19.95M, Image = "soadHypnotize.webp", Stock = 15, ProductAuthorId = 3, ProductGenreId = 2 });
            await context.Products.AddAsync(new Product() { Name = "I Want To Die In New Orleans Vinyl LP", Description = "text", Price = 22, Image = "suicideboysIWTDINO.webp", Stock = 5, ProductAuthorId = 1, ProductGenreId = 1 });
            await context.Products.AddAsync(new Product() { Name = "Sing Me A Lullaby, My Sweet Temptation Vinyl LP", Description = "text", Price = 24.95M, Image = "suicideboysMST.webp", Stock = 5, ProductAuthorId = 1, ProductGenreId = 1 });
            await context.Products.AddAsync(new Product() { Name = "Illmatic Vinyl LP", Description = "text", Price = 27.95M, Image = "nasIllmatic.webp", Stock = 15, ProductAuthorId = 4, ProductGenreId = 1 });
            await context.Products.AddAsync(new Product() { Name = "It Was Written Vinyl 2LP", Description = "text", Price = 34.95M, Image = "nasIWW.webp", Stock = 15, ProductAuthorId = 4, ProductGenreId = 1 });
            await context.Products.AddAsync(new Product() { Name = "I Am... Vinyl 2LP", Description = "text", Price = 24.95M, Image = "nasIam.webp", Stock = 15, ProductAuthorId = 4, ProductGenreId = 1 });
            await context.Products.AddAsync(new Product() { Name = "Nastradamus Vinyl 2LP", Description = "text", Price = 24.95M, Image = "nasNastradamus.webp", Stock = 15, ProductAuthorId = 4, ProductGenreId = 1 });
            await context.Products.AddAsync(new Product() { Name = "God's Son Vinyl 2LP", Description = "text", Price = 31.95M, Image = "nasGodsSon.webp", Stock = 15, ProductAuthorId = 4, ProductGenreId = 1 });
            await context.Products.AddAsync(new Product() { Name = "Nasir Vinyl LP", Description = "text", Price = 21.95M, Image = "nasNasir.webp", Stock = 15, ProductAuthorId = 4, ProductGenreId = 1 });
            await context.Products.AddAsync(new Product() { Name = "Magic Vinyl LP", Description = "text", Price = 24.95M, Image = "nasMagic.webp", Stock = 15, ProductAuthorId = 4, ProductGenreId = 1 });
            await context.Products.AddAsync(new Product() { Name = "MM..Food Vinyl 2LP", Description = "text", Price = 34.95M, Image = "mfdoomMMFood.webp", Stock = 15, ProductAuthorId = 5, ProductGenreId = 1 });
            await context.Products.AddAsync(new Product() { Name = "Madvillainy Vinyl 2LP", Description = "text", Price = 24.95M, Image = "madvillainMadvillainy.webp", Stock = 15, ProductAuthorId = 5, ProductGenreId = 1 });
            await context.Products.AddAsync(new Product() { Name = "Mouse & The Mask - Metalface Edition Vinyl 2LP", Description = "text", Price = 34.95M, Image = "dangerdoomM&TM.webp", Stock = 15, ProductAuthorId = 5, ProductGenreId = 1 });
            await context.Products.AddAsync(new Product() { Name = "Gorillaz Vinyl 2LP", Description = "text", Price = 24.95M, Image = "gorillazGorillaz.webp", Stock = 15, ProductAuthorId = 6, ProductGenreId = 3 });
            await context.Products.AddAsync(new Product() { Name = "Demon Days Vinyl 2LP", Description = "text", Price = 29.95M, Image = "gorillazDD.webp", Stock = 15, ProductAuthorId = 6, ProductGenreId = 3 });
            await context.Products.AddAsync(new Product() { Name = "Whatever People Say I Am, That's What I'm Not Vinyl LP", Description = "text", Price = 24.95M, Image = "arcticmonkeysWPSIA.webp", Stock = 15, ProductAuthorId = 7, ProductGenreId = 4 });
            await context.Products.AddAsync(new Product() { Name = "Man On The Moon - The End Of Day Vinyl 2LP", Description = "text", Price = 34.95M, Image = "kidcudiMOTM.webp", Stock = 15, ProductAuthorId = 8, ProductGenreId = 3 });
            await context.Products.AddAsync(new Product() { Name = "Man On The Moon II - The Legend Of Mr. Rager Vinyl 2LP", Description = "text", Price = 24.95M, Image = "kidcudiMOTM2.webp", Stock = 15, ProductAuthorId = 8, ProductGenreId = 3 });
            await context.Products.AddAsync(new Product() { Name = "Man On The Moon III - The Chosen Vinyl 2LP", Description = "text", Price = 27.95M, Image = "kidcudiMOTM3.webp", Stock = 15, ProductAuthorId = 8, ProductGenreId = 3 });
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
