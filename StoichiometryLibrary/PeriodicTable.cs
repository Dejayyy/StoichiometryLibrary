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
        public IElement[]? Elements { get; private set; }

        public void LoadElements()
        {
            try
            {
                string jsonText = File.ReadAllText("PeriodicTableJSON.json");
                var jsonObject = JObject.Parse(jsonText);
                Elements = jsonObject["elements"]!.ToObject<Element[]>();
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("Failed to load periodic table data", ex);
            }
        }
    }
}