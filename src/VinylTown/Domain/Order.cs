namespace VinylTown.Domain
{
    public class Order
    {
        public int Id { get; set; }
        public string CustomerId { get; set; }
        public decimal Amout { get; set; }
        public DateTimeOffset OrderDate { get; private set; } = DateTimeOffset.Now;
        public Address Address { get; set; }
        public List<OrderItem> OrderItems { get; set; }
    }
}
