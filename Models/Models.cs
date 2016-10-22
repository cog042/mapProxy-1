
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
// using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;

public class DB : DbContext {

    public DB(): base(){}
    // public DB(IDbContextFactory<DB> factory): base(){}

    // protected override void OnModelCreating(ModelBuilder builder)
    // {
    //     base.OnModelCreating(builder);
    // }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) {
        // optionsBuilder.UseSqlite(Configuration.GetConnectionString("DefaultConnection"));
        // Sqlite can also take other connection strings; 
        // Exeternal data sources can also be used:
        // i.e. 
        // @"Data Source=d:\otherdrive.db"
        // @"Data Source=http://...."
        optionsBuilder.UseSqlite("Filename=./app.db");
    }

    protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.Entity<Blog>().ToTable("Blog"); // rename Blogs table to Blog
            builder.Entity<Post>().ToTable("Post"); // rename Posts table to Post
        }

    public DbSet<Blog> Blogs { get; set; }
    public DbSet<Post> Posts { get; set; }
}

public class Blog
{
    public Guid BlogId { get; set; }
    public string Url { get; set; }
    public List<Post> Posts { get; set; }
}

public class Post
{
    public Guid PostId { get; set; }
    public string Title { get; set; }
    public string Content { get; set; }
    public Blog Blog { get; set; }
}

public static class Seed
{
    public static void Initialize(DB db)
    {
        db.Database.EnsureCreated();
        db.Database.EnsureDeleted();
        Console.WriteLine("-----------------------");
        Console.WriteLine(db.Blogs.ToList().Count);
        Console.WriteLine(db.Posts.ToList().Count);

        foreach(var b in db.Blogs.ToList()){
            Console.WriteLine($"- {b.Url}, {b.Posts.ToList().Count}");
        }
        
        // Look for any Posts.
        if (db.Posts.Any())
        {
            return; // DB has been seeded already
        }
        
        List<Post> posts = new List<Post>();
        Blog mine = new Blog {Url = "mkeas.org"};
        db.Blogs.Add(mine);
        for(var i = 0; i < 10; i++){
            db.Posts.Add(new Post { Title = $"Test Post {i}", Content = $"Test Content {i}", Blog = mine }); // Date=DateTime.Parse("2005-09-01")},
        }

        // Console.WriteLine(db.Database);
        db.SaveChanges();
    }
}