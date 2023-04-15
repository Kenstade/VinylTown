using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using VinylTown.Features.Cart;

namespace VinylTown.Controllers;

[Authorize, Route("cart")]
public class CartController : Controller
{
    private readonly IMediator _mediator;
    public CartController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<IActionResult> Index()
    {
        var viewModel = await _mediator.Send(new GetCartQuery());
        return View(viewModel);
    }

    [HttpPost("add-item")]
    public async Task<IActionResult> AddItem(CartItemViewModel cartItem)
    {
        await _mediator.Send(new AddItemCommand { CartItem = cartItem});

        return RedirectToAction("Index");
    }
}
