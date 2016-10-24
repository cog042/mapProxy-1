using System;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

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

Each Route will respond to all HTTP Verbs unless you explicitly use a particular attribute for the verb you want:

[HttpPost] / [HttpPost("sub/route/{id}")]
[HttpGet] / [HttpGet("sub/route/{id}")]
[HttpPut] / [HttpPut("sub/route/other/route")]
[HttpDelete] / etc

A Controller can define a root route, too! Then your route methods can define the subroute:

[Route("/mycontroller")]
class MyController : Controller {
    [HttpGet("{id?}")]
    public IActionResult Get(int? id){
        // if id provided, return that single instance
        if(id.HasValue)
            return Ok(db.Posts.FIrstOrDefault(e => e.Id === id.Value));
        // else return all items in the collection
        return Ok(db.Posts);
    }
}

Post/Put routes can also have an attribute ([FromBody]) to parse form body-data into a model instance:

[HttpPost]
public IActionResult Post([FromBody]Post p){
    db.Posts.Add(p);
    db.SaveChanges();
    return Created("Get", p);
}

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

[Route("/api/posts/")]
public class PostController : Controller
{
    private DB db;

    public PostController(DB db){
        this.db = db;
    }

    [HttpGet]
    public IActionResult Get() =>
        Ok(db.Posts.OrderBy(p => p.Title).ToList());

    [HttpGet("{id}")]
    public IActionResult Get(int id) =>
        Ok(db.Posts.FirstOrDefault(p => p.PostId == id));

    [HttpGet("test/{id}")]
    public IActionResult SQL(int id) {
        return Ok(db.Posts.FromSql($"select * from dbo.Post where PostId = {id}").ToList()); // only works with SQL, not in-memory
    }

    [HttpPost]
    public IActionResult Post([FromBody]Post p){
        db.Posts.Add(p);
        db.SaveChanges();
        return Ok(p);
    }
}