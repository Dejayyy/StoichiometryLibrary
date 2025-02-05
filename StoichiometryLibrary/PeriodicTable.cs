using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;

namespace StoichiometryLibrary
{
    public class PeriodicTable
    {
        private static IElement[]? _elements;
        public static IElement[] Elements => _elements ?? throw new InvalidOperationException("Elements not loaded");

        static PeriodicTable()
        {
            try
            {
                string jsonText = File.ReadAllText("PeriodicTableJSON.json");
                var jsonObject = JObject.Parse(jsonText);
                _elements = jsonObject["elements"]!.ToObject<Element[]>();
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("Failed to load periodic table data", ex);
            }
        }
    }
}