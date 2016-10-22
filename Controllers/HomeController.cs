using Microsoft.AspNetCore.Mvc;

public class HomeController : Controller
{
    [HttpGet]
    [Route("/")]
    public IActionResult Root(){
        return Ok("ok.");
    }

    [HttpGet]
    [Route("Home/Index")]
    public IActionResult Index()
    {
        return View(new { Username = "you" });
    }

    [HttpGet]
    [Route("Home/Index/test/{username?}")]
    public IActionResult Index(string username = "you")
    {
        return View(new { Username = username });
    }

    [HttpGet]
    [Route("Home/Greet/{username}")]
    public IActionResult Greet(string username){
        return Ok(new { Username = username });
    }
}