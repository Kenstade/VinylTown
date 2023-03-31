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
    public async Task<IActionResult> Index(int? author,string search = "", int page = 1)
    {
        var viewModel = await _mediator.Send(new GetProductsQuery { PageNumber = page, Search = search, AuthorId = author });
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
}
