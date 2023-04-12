using MediatR;
using Microsoft.EntityFrameworkCore;
using VinylTown.Data;
using VinylTown.Interfaces;

namespace VinylTown.Features.Cart;

public record GetCartQuery : IRequest<CartViewModel>;

public class CartViewModel
{
    public int Id { get; set; }
    public string CostumerId { get; set; }
    public decimal Total { get; set; }
    public decimal Discount { get; set; }
    public IEnumerable<CartItemViewModel> Items { get; set; } = new List<CartItemViewModel>();
}

public class CartItemViewModel
{
    public int Id { get; set; }
    public int ProductId { get; set; }
    public string Name { get; set; } = null!;
    public int Quantity { get; set; }
    public decimal Price { get; set; }
    public string Image { get; set; } = null!;
}

public class GetCartQueryHanlder : IRequestHandler<GetCartQuery, CartViewModel>
{
    private readonly AppDbContext _context;
    private readonly IUserService _user;
    public GetCartQueryHanlder(AppDbContext context, IUserService user)
    {
        _context = context;
        _user = user;
    }

    public async Task<CartViewModel> Handle(GetCartQuery request, CancellationToken cancellationToken)
    {
        var cart = await _context.CostumerCarts
           .Include(c => c.Items)
           .AsNoTracking()
           .FirstOrDefaultAsync(c => c.CostumerId == _user.GetUserId());


        if (cart == null)
        {
            return new CartViewModel();
        }

        return new CartViewModel
        {
            Id = cart.Id,
            CostumerId = cart.CostumerId,
            Total = cart.Total,
            Discount = cart.Discount,
            Items = cart.Items.Select(ci => new CartItemViewModel
            {
                Id = ci.Id,
                ProductId = ci.ProductId,
                Name = ci.ProductName,
                Quantity = ci.Quantity,
                Price = ci.Price,
                Image = ci.Image,
            }).ToArray()
        };
    }

}




