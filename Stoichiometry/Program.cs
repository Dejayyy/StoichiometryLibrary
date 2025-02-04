using StoichiometryLibrary;
using System;

namespace Stoichiometry
{
    internal class Program
    {
        static void Main(string[] args)
        {
            PeriodicTable table = new PeriodicTable();
            table.LoadElements();

            Console.WriteLine(table.Elements[1].Name);
        }
    }
}