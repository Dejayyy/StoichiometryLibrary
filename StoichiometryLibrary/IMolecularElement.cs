/**
 * Program: IMolecularElement.cs
 * Author: Logan McCallum, Ayden Nicholson, William Mouhtouris
 * Date: Feb 2nd, 2024
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoichiometryLibrary
{
    public interface IMolecularElement : IElement
    {
        ushort Multiplier { get; }
    }
}
