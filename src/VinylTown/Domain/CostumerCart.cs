namespace VinylTown.Domain;

public class CostumerCart
{
    public int Id { get; set; }
    public string CostumerId { get; set; }
    public decimal Total { get; set; }
    public decimal Discount { get; set; }
    public List<CartItem> Items { get; set; } = new();
}
