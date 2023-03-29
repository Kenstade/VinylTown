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
    public async Task<IActionResult> Index(string search = "", int page = 1)
    {
        var viewModel = await _mediator.Send(new GetProductsQuery { PageNumber = page, Search = search });
        ViewBag.Search = search;
        return View(viewModel);
    }

    [HttpGet("products/{id}")]
    public async Task<IActionResult> ProductDetails(int id)
    {
        if (id <= 0)
        {
            return BadRequest();
        }

        var viewModel = await _mediator.Send(new GetProductDetailsQuery { Id = id });

        if (viewModel == null)
        {
            return BadRequest("afesdf");
        }
        return View(viewModel);
    }

    //[HttpPost]
    //public async Task<IActionResult> CreateProduct(CreateProductCommand command)
    //{
    //    var viewModel = await _mediator.Send(command);
    //    return View(viewModel);
    //}

    //[HttpPut("id")]
    //public async Task<IActionResult> UpdateProductAsync(UpdateProductCommand command)
    //{
    //    return Ok(await _mediator.Send(command));
    //}

    //[HttpDelete("id")]
    //public async Task<IActionResult> DeleteProductAsync(int id)
    //{
    //    await _mediator.Send(new DeleteProductCommand(id));

    //    return NoContent();
    //}
}
