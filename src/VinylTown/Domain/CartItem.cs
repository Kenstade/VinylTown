namespace VinylTown.Domain;

public class CartItem
{
    public int Id { get; set; }
    public int ProductId { get; set; }
    public string ProductName { get; set; }
    public decimal Price { get; set; }
    public int Quantity { get; set; }
    public string Image { get; set; }
    public int CostumerCartId { get; set; }
    public CostumerCart CostumerCart { get; set; }

}


