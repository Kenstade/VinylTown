using System.Diagnostics;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using VinylTown.Features.Products;

namespace VinylTown.Controllers;

public class CatalogController : Controller
{
    private readonly IMediator _mediator;
    public CatalogController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<IActionResult> Index()
    {
        var viewModel = await _mediator.Send(new GetProductsQuery());

        return View(viewModel);
    }

    [HttpPost]
    public async Task<IActionResult> CreateProduct(CreateProductCommand command)
    {
        var viewModel = await _mediator.Send(command);
        return View(viewModel);
    }

    [HttpPut("id")]
    public async Task<IActionResult> UpdateProductAsync(UpdateProductCommand command)
    {
        return Ok(await _mediator.Send(command));
    }

    [HttpDelete("id")]
    public async Task<IActionResult> DeleteProductAsync(int id)
    {
        await _mediator.Send(new DeleteProductCommand(id));

        return NoContent();
    }
}
