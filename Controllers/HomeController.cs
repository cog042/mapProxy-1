using Microsoft.AspNetCore.Mvc;

public class HomeController : Controller
{
    [Route("/")]
    public IActionResult Root(){
        return Ok("ok.");
    }

    [Route("Home/Index")]
    public IActionResult Index()
    {
        return View(new { Username = "you" });
    }

    [Route("Home/Index/test/{username?}")]
    public IActionResult Index(string username = "you")
    {
        return View(new { Username = username });
    }

    [Route("Home/Greet/{username}")]
    public IActionResult Greet(string username){
        return Ok(new { Username = username });
    }
}