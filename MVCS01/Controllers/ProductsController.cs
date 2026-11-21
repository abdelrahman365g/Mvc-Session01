using Microsoft.AspNetCore.Mvc;
using MVCS01.Models;

namespace MVCS01.Controllers;

public class ProductsController: Controller
{
    private static readonly List<Product> _products = new List<Product>
    {
        new Product { Id = 1, Name = "Mobile" },
        new Product { Id = 2, Name = "Tablet" }
    };

    public IActionResult Index()
    {
        return View(_products);
    }

    public IActionResult Test()
    {
        ContentResult result = new ContentResult();
        result.ContentType = "text/html";
        result.Content = $"Hello from test </br> Bye";
        result.StatusCode = StatusCodes.Status200OK;
        return result;
    }
    
    public IActionResult GetProducts()
    {
        return Json(_products);
    }
    
    public IActionResult GetProductById(int id)
    {
        if (id < 0)
            return BadRequest();
        if (id == 0)
            return RedirectToAction(nameof(GetProducts));

        Product? product = _products.FirstOrDefault(p => p.Id == id);
        if (product is null)
            return NotFound();

        return Json(product);
    }
    
    public IActionResult RedirectAction()
    {
        return Redirect("https://github.com/abdelrahman365g/Mvc-Session01.git");
    }

    public IActionResult FileAction()
    {
        byte[] bytes = System.IO.File.ReadAllBytes("wwwroot/files/link.txt");
        return File(bytes, "text/plain", "GithubLink.txt");
    }

    [NonAction]
    public IActionResult Helper()
    {
        return View();
    }
    
    [HttpGet("Products/Test01")]
    public IActionResult TestRouting()
    {
        return Json("Hello from TestRouting");
    }
    
    [HttpGet("TestingRoute/Test{id:int}")]
    public IActionResult TestRoutingWithId(int id)
    {
        return Json($"Hello with id : {id}");
    }

    [HttpGet("json")]
    public IActionResult JsonAction()
    {
        return Json("Hello from json action");
    }

    [HttpGet("ProductCode/{code:int}")]
    public IActionResult GetProductByCode(int code)
    {
        return Json(new Product { Id = code, Name = "Mobile" });
    }
    
    public IActionResult GetById([FromRoute] int id, [FromQuery(Name = "id2")] int anotherId)
    {
        return Json(new { id, anotherId });
    }

    public IActionResult Search(ProductFilter filter)
    {
        return Json(filter);
    }

    public IActionResult GetByIds(int[] ids)
    {
        return Json(new { ids });
    }
}