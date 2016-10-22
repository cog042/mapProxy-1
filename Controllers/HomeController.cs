using Microsoft.AspNetCore.Mvc;

[Route("/")]
public class HomeController : Controller
{
    [HttpGet]
    public IActionResult Root(){
        return Ok("ok.");
    }

    [HttpGet("Home/Index")]
    public IActionResult Index()
    {
        return View(new { Username = "you" });
    }

    [HttpGet("Home/Index/test/{username?}")]
    public IActionResult Index(string username = "you")
    {
        return View(new { Username = username });
    }

    [HttpGet("Home/Greet/{username}")]
    public IActionResult Greet(string username){
        return Ok(new { Username = username });
    }

    [HttpGet("/Errors/{errCode}")]
    public IActionResult Errors(string errCode) 
    { 
        return (errCode == "500" | errCode == "404")
            ? View($"~/Views/Home/Error/{errCode}.cshtml")
            : View("~/Views/Shared/Error.cshtml");
    }
}