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
    public async Task<IActionResult> Index(int? genre,string search = "", int page = 1)
    {
        var viewModel = await _mediator.Send(new GetProductsQuery { PageNumber = page, Search = search, Genre = genre });
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

    [HttpGet("search")]
    public async Task<IActionResult> Search(string query, int pageNumber = 0, int pageSize = 12) 
    {
        var getProductsQuery = new GetProductsQuery
        {
            Search = query,
            PageNumber = pageNumber,
            PageSize = pageSize
        };

        var vm = await _mediator.Send(getProductsQuery);

        return new JsonResult(vm);
    }
}
