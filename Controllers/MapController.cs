using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System;
using System.Net.Http;
using Newtonsoft.Json;
using GoogleStaticMapsX;


[Route("/api/map")]
public class MapController : Controller {
    private IGoogleRepo googleApi;
    private IGoogleSMRepo googleStatic;
    
    public MapController(IGoogleRepo g){
        googleApi = g;
    }
    
    [HttpGet("{search}")]
    public async Task<IActionResult> Get(string search, [FromQuery]string size = "800x600", [FromQuery]int zoom = 13){
        
        var key = "AIzaSyCdNJl4Lm98QbzuYKdbcn9gCYawkmc_tlk";
        var url = $"https://maps.googleapis.com/maps/api/geocode/json?address={search}&key={key}";
        Google result = await googleApi.GetData<Google>(url);
            double lat = result.results.ElementAt(0).geometry.location.lat;  //not sure we need this bit yet.
            double lng = result.results.ElementAt(0).geometry.location.lng;
            String LatLng = (lat.ToString()+","+lng.ToString());
        
        // return Ok(new {search = search, size = size, result = result});
        GoogleStaticMaps GSmaps = new GoogleStaticMaps();
        GSmaps.imageUrl = $"https://maps.googleapis.com/maps/api/staticmap?zoom={zoom}&size={size}&maptype=roadmap&markers=color:red|label:X|{LatLng}";
        GSmaps.timestamp = DateTime.Now.ToString();
        GSmaps.searchTerm = search;
        GSmaps.zoomLevel = zoom;
        GSmaps.size = new Size {
            width = Int32.Parse(size.Split(new char[]{ 'x' })[0]),
            height = Int32.Parse(size.Split(new char[]{ 'x' })[1])
        };
        GSmaps.markers = new List<Marker> {
            new Marker {
                color = "red",
                label = "X",
                location = new GoogleStaticMapsX.Location {
                    lat = lat,
                    lng = lng
                }
            }
        };
        return Ok(GSmaps);
        // Console.WriteLine(googleApi.ToJSON(GSmaps));
    }
}

