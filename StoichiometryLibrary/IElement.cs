﻿/**
 * Program: IElement.cs
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
    //Defines the properties of the chemical element
    public interface IElement
    {
        string Symbol { get; }
        string Name { get; }
        ushort AtomicNumber { get; }
        double AtomicMass { get; }
        ushort Period { get; }
        ushort Group { get; }
    }
}
