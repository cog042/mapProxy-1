using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System;
using System.Net.Http;
using Newtonsoft.Json;

[Route("/api/map")]
public class MapController : Controller {
    private IGoogleRepo googleApi;
    public MapController(IGoogleRepo g){
        googleApi = g;
    }

    [HttpGet("{search}")]
    // http://localhost:5000/api/map/Woodbar?size=800x600
    public async Task<IActionResult> Get(string search, string size){
        // 1. make request to Google
        var key = "AIzaSyCixfkHHa7NI0Pi4_SHrL8wZS3VZiGek9s";
        Google result = await googleApi.GetData<Google>($"https://maps.googleapis.com/maps/api/geocode/json?address={search}&key={key}");
        // 2. deserialize request from Google   check

        // 3. return new structure back as Object
        return Ok(new {search = search, size = size, result = result});
        // string result = await client.GetStringAsync()
        // JSON...
    }   
}