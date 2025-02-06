/**
 * Program: Element.cs
 * Author: Logan McCallum, Ayden Nicholson, William Mouhtouris
 * Date: Feb 2nd, 2024
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace StoichiometryLibrary
{
    //Represents a chemical element properties mapped to the JSON.
    internal class Element : IMolecularElement
    {
        [JsonProperty("symbol")]
        public string Symbol { get; private set; }

        [JsonProperty("name")]
        public string Name { get; private set; }

        [JsonProperty("number")]
        public ushort AtomicNumber { get; private set; }

        [JsonProperty("atomic_mass")]
        public double AtomicMass { get; private set; }

        [JsonProperty("period")]
        public ushort Period { get; private set; }

        [JsonProperty("group")]
        public ushort Group { get; private set; }

        public ushort Multiplier { get; private set;  } = 1;

        //Constructors
        public Element(string symbol, string name, ushort atomicNumber, double atomicMass, ushort period, ushort group)
        {
            Symbol = symbol;
            Name = name;
            AtomicNumber = atomicNumber;
            AtomicMass = atomicMass;
            Period = period;
            Group = group;
        }

        //Handle Multipliers
        public Element AddMultiplier(ushort multiplier)
        {
            return new Element(Symbol, Name, AtomicNumber, AtomicMass, Period, Group) { Multiplier = multiplier };
        }
    }
}
