using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace StoichiometryLibrary
{
    internal class Element : IMolecularElement
    {
        [JsonProperty("symbol")]  ///MIGHT BE WRONG, NOT SURE IF ELEMENT IS ACTUALLY HIDDEN ATM 
        private string _symbol;

        [JsonProperty("name")]
        private string _name;

        [JsonProperty("number")]
        private ushort _atomicNumber;

        [JsonProperty("atomic_mass")]
        private double _atomicMass;

        [JsonProperty("period")]
        private ushort _period;

        [JsonProperty("group")]
        private ushort _group;

        public ushort Multiplier { get; } = 1;

        public string Symbol => _symbol;
        public string Name => _name;
        public ushort AtomicNumber => _atomicNumber;
        public double AtomicMass => _atomicMass;
        public ushort Period => _period;
        public ushort Group => _group;
    }
}
