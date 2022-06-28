using Microsoft.AspNetCore.Mvc.ViewFeatures;
using System.Text.Json;

namespace MyApp.Web.Utilities
{
    //extension for TempData
    public static class TempDataExtensions
    {
        //store the response message in TempData
        public static void Put<T>(this ITempDataDictionary tempData, string key, T value) where T: class
        {
            tempData[key] = JsonSerializer.Serialize(value);
        }

        //Work as flush message
        public static T Get<T>(this ITempDataDictionary tempData, string key) where T : class
        {
            object o;
            tempData.TryGetValue(key, out o);
            return o == null ? null : JsonSerializer.Deserialize<T>((string)o);
        }

        public static T Peek<T>(this ITempDataDictionary tempData, string key) where T: class
        {
            object o = tempData.Peek(key);
            return o == null ? null : JsonSerializer.Deserialize<T>((string)o);
        }
    }
}
