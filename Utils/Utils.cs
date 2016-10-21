using System;
using Newtonsoft.Json;

internal static class Utils {
    public static void Log(this Object me){
        Console.WriteLine(JsonConvert.SerializeObject(me));
    }
}