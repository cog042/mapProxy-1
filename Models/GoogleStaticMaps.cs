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

namespace GoogleStaticMapsX{
    //https://maps.googleapis.com/maps/api/geocode/json?address={search}&key={your_api_key}
    //key:  AIzaSyCdNJl4Lm98QbzuYKdbcn9gCYawkmc_tlk

    public interface IGoogleSMRepo {              //build interface to use 
        Task<string> GetJSON(string url);
        Task<T> GetData<T>(string url);
        string ToJSON(Object o);
        
    }
    public class Size
    {
        public int width { get; set; }
        public int height { get; set; }
    }

    public class Location
    {
        public double lat { get; set; }
        public double lng { get; set; }
    }

    public class Marker
    {
        public string color { get; set; }
        public string label { get; set; }
        public Location location { get; set; }
    }

    public class GoogleStaticMaps
    {
        public string imageUrl { get; set; }
        public string timestamp { get; set; }
        public string searchTerm { get; set; }
        public int zoomLevel { get; set; }
        public Size size { get; set; }
        public List<Marker> markers { get; set; }
    }

    public class GoogleSMRepo : IGoogleSMRepo
    {
        public async Task<string> GetJSON(string url) =>
            await new HttpClient().GetStringAsync(url);
        public async Task<T> GetData<T>(string url) => 
            JsonConvert.DeserializeObject<T>( await GetJSON(url) );

        public string ToJSON(Object o) => JsonConvert.SerializeObject(o);

    }
}
