using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace VinylTown.Controllers;

public class CatalogController : Controller
{
    public IActionResult Index()
    {
        return View();
    }
}
