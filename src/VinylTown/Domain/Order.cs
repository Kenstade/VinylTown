namespace VinylTown.Domain;

public class Order
{
    public int Id { get; set; }
    public string CustomerId { get; set; }
    public decimal Total { get; set; }
    public DateTimeOffset OrderDate { get; private set; } = DateTimeOffset.Now;
    public OrderStatus OrderStatus { get; set; }
    public string Street { get; private set; }
    public string City { get; private set; }
    public string Country { get; private set; }
    public string ZipCode { get; private set; }
    public List<OrderItem> OrderItems { get; set; }
}

