using Microsoft.AspNetCore.Mvc;

public class PostController : Controller
{
    private DB db;

    public PostController(DB db){
        this.db = db;
    }

    /* 
    
    A route can return a view, or a status message with data
    
    return View(new { Username = "you" });
    return Ok("ok.");
    return Ok(new { some="data", can="be sent here" });

    A route can talk to the db and return JSON

    // Querying
    var blogs = db.Blogs
        .Where(b => b.Rating > 3)
        .OrderBy(b => b.Url)
        .ToList();
    
    return Ok(blogs);

    A route can take arguments and save to the db
    
    // Saving
    var blog = new Blog { Url = "http://sample.com" };
    db.Blogs.Add(blog);
    db.SaveChanges();

    Amongst these actions, a route can create new model instances, and save them to
    the db or it can pass them along to a View:

    return View(new Greeting { Username = "you" });
    
    Routes can be written to handle URI options:

    [Route("/posts")]
    [Route("/posts/{id}")]
    [Route("/posts/{id}/{username?}")]

    These are passed into the method as named arguments:

    public IActionResult GetPosts(string id, string? username){...}

    GET params are passed as arguments to the Route method

    [Route("/posts")]
    public IActionResult Get(string filter = "tutorials"){ 
        // if ?filter=videos is in the URL, filter above becomes "videos"
    }

    Each Route will respond to all HTTP Verbs unless you explicitly limit it with an Attribute:

    [HttpPost]
    [HttpGet]
    [HttpPut]
    [HttpDelete]

    Each Route that handles Post/Put data may also need some extra security measures:

    [ValidateAntiForgeryToken]

    Route methods can be async:

    public async Task<IActionResult> GetPosts(...){
        var posts = await GetPostsFromDB();
    }

    There are also other properties and methods available inside Route methods:
    ViewData - Dictionary<string, string> that is accessible inside View()s
    ModelState - contains the model instance being passed into the route (auto-parsed by MVC),
        has properties like ModelState.IsValid
    RedirectToLocal(url) - redirect a client to a different url on the server
    AddErrors(...) - build up errors to send

    */

    [HttpGet]
    [Route("/posts")]
    public IActionResult Get(){
        return Ok("ok.");
    }
}