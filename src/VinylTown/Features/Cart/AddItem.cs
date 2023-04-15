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
           .Include(c => c.Items)
           .AsNoTracking()
           .FirstOrDefaultAsync(c => c.CostumerId == _user.GetUserId());

        //if(cart == null)
        //{
        //    cart = new CostumerCart { CostumerId = _user.GetUserId() };
        //    cart.Items.Add(request.CartItem);
            
        //}

        return Unit.Value;
    }
}
