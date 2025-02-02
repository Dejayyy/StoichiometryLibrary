using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoichiometryLibrary
{
    public interface IElement
    {
        string Symbol { get; }
        string Name { get; }
        ushort AtomicNumber { get; }
        double atomicMass { get; }
        ushort Period { get; }
        ushort Group { get; }
    }
}
