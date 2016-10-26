using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
// using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using System.Net.Http;
using Newtonsoft.Json;

//https://maps.googleapis.com/maps/api/geocode/json?address={search}&key={your_api_key}
//key:  AIzaSyCdNJl4Lm98QbzuYKdbcn9gCYawkmc_tlk

public interface IGoogleRepo {              //build interface to use 
    Task<string> GetJSON(string url);
    Task<T> GetData<T>(string url);
    string ToJSON(Object o);
    
}
public class AddressComponent
{
    public string long_name { get; set; }
    public string short_name { get; set; }
    public List<string> types { get; set; }
}

public class Northeast
{
    public double lat { get; set; }
    public double lng { get; set; }
}

public class Southwest
{
    public double lat { get; set; }
    public double lng { get; set; }
}

public class Bounds
{
    public Northeast northeast { get; set; }
    public Southwest southwest { get; set; }
}

public class Location
{
    public double lat { get; set; }
    public double lng { get; set; }
}

public class Northeast2
{
    public double lat { get; set; }
    public double lng { get; set; }
}

public class Southwest2
{
    public double lat { get; set; }
    public double lng { get; set; }
}

public class Viewport
{
    public Northeast2 northeast { get; set; }
    public Southwest2 southwest { get; set; }
}

public class Geometry
{
    public Bounds bounds { get; set; }
    public Location location { get; set; }
    public string location_type { get; set; }
    public Viewport viewport { get; set; }
}

public class Result
{
    public List<AddressComponent> address_components { get; set; }
    public string formatted_address { get; set; }
    public Geometry geometry { get; set; }
    public bool partial_match { get; set; }
    public string place_id { get; set; }
    public List<string> types { get; set; }
}

public class Google {
    public List<Result> results { get; set; }
    public string status { get; set; }
}

public class GoogleRepo : IGoogleRepo
{
    public async Task<string> GetJSON(string url) =>
        await new HttpClient().GetStringAsync(url);
    public async Task<T> GetData<T>(string url) => 
        JsonConvert.DeserializeObject<T>( await GetJSON(url) );
    
    public string ToJSON(Object o) => JsonConvert.SerializeObject(o);

}
