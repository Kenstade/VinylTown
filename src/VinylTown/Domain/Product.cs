namespace VinylTown.Domain;

public class Product
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string? Description { get; set; }
    public decimal Price { get; set; }
    public string Image { get; set; } = string.Empty;
    public int Stock { get; set; }
    public int ProductAuthorId { get; set; }
    public ProductAuthor ProductAuthor { get; set; } = null!;
    public int ProductGenreId { get; set; }
    public ProductGenre ProductGenre { get; set; } = null!;
}
