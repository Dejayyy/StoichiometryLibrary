using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoichiometryLibrary
{
    public class PeriodicTable
    {
        [JsonProperty("elements")]
        public static IElement[]? Elements { get; }

        public  IElement[] LoadElements()
        {
            try
            {
                string jsonText = File.ReadAllText("PeriodicTableJSON.json");
                var jsonObject = JObject.Parse(jsonText);
                Element[]? elements = jsonObject["elements"]!.ToObject<Element[]>();
                return elements!;
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("Failed to load periodic table data", ex);
            }
        }
    }
}
