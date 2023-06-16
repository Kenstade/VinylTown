using MediatR;
using Microsoft.EntityFrameworkCore;
using VinylTown.Data;
using VinylTown.Domain;
using VinylTown.Interfaces;

namespace VinylTown.Features.Cart;

public record AddItemCommand : IRequest
{
    public CartItemViewModel CartItem { get; set; }
}

public class AddItemCommandHandler : IRequestHandler<AddItemCommand>
{
    private readonly AppDbContext _context;
    private readonly IUserService _user;
    public AddItemCommandHandler(AppDbContext context, IUserService user)
    {
        _context = context;
        _user = user;
    }

    public async Task<Unit> Handle(AddItemCommand request, CancellationToken cancellationToken)
    {
        var cart = await _context.CostumerCarts
           .AsNoTracking()
           .FirstOrDefaultAsync(c => c.CostumerId == _user.GetUserId());

        if (cart == null)
        {
            cart = new CostumerCart 
            { 
                CostumerId = _user.GetUserId(), 
            };
            _context.CostumerCarts.Add(cart);
            
            _context.CartItems.Add(new CartItem()
            {
                ProductId = request.CartItem.ProductId,
                ProductName = request.CartItem.Name,
                Price = request.CartItem.Price,
                Image = request.CartItem.Image,
                Quantity = 1,
                CostumerCartId = cart.Id,
            });

        }
        else
        {
            var cartItem = await _context.CartItems
                .AsNoTracking()
                .FirstOrDefaultAsync(c => c.CostumerCartId == cart.Id && c.ProductId == request.CartItem.Id);
            if(cartItem == null)
            {
                _context.CartItems.Add(new CartItem()
                {
                    ProductId = request.CartItem.ProductId,
                    ProductName = request.CartItem.Name,
                    Price = request.CartItem.Price,
                    Image = request.CartItem.Image,
                    Quantity = 1,
                    CostumerCartId = cart.Id,
                });
            }
            else
            {
                cartItem.Quantity += 1;
                _context.Update(cartItem);
            }

        }


        await _context.SaveChangesAsync();

        return Unit.Value;
    }
}
