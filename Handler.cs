using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
// using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

public class Handler {

    public IConfigurationRoot Configuration { get; }

    public Handler(IHostingEnvironment env)
    {
        var builder = new ConfigurationBuilder()
            .SetBasePath(env.ContentRootPath)
            .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
            .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true);

        // if (env.IsDevelopment())
        // {
        //     // For more details on using the user secret store see https://go.microsoft.com/fwlink/?LinkID=532709
        //     builder.AddUserSecrets();
        // }

        builder.AddEnvironmentVariables();
        Configuration = builder.Build();
    }

    public void ConfigureServices(IServiceCollection services)
    {

        services.AddDbContext<DB>(options =>
            options.UseSqlite(Configuration.GetConnectionString("DefaultConnection")));

        // services.AddIdentity<User, IdentityRole>()
        //     .AddEntityFrameworkStores<DB>()
        //     .AddDefaultTokenProviders();

        services.AddMvc();
    }

    public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory logger, DB db) {
        // var logger = factory.CreateLogger("Catchall Endpoint");
        logger.AddConsole(Configuration.GetSection("Logging"));
        logger.AddDebug();

        if (env.IsDevelopment())
        {
            // app.UseDeveloperExceptionPage();
            // app.UseDatabaseErrorPage();
            // app.UseBrowserLink();
        }
        else
        {
            app.UseExceptionHandler("/Error");
        }

        app.UseStaticFiles();
        // app.UseIdentity();
        app.UseMvc();
        // Seed.Initialize(db);
    }

}