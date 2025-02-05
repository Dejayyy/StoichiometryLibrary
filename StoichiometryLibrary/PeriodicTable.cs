using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;

namespace StoichiometryLibrary
{
    public class PeriodicTable
    {
        [JsonProperty("elements")]
        public static IElement[]? Elements { get; private set; }

        static PeriodicTable()
        {
            try
            {
                string jsonText = File.ReadAllText("PeriodicTableJSON.json");

                // parse
                var jsonObject = JObject.Parse(jsonText);

                // extract elements
                var elementsArray = jsonObject["elements"]?.ToObject<List<Element>>();
                
                // convert and put into array
                Elements = elementsArray?.ToArray();
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("Failed to load periodic table data", ex);
            }
        }
    }
}