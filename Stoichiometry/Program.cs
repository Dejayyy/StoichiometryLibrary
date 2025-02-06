/**
 * Program: Program.cs
 * Author: Logan McCallum, Ayden Nicholson, William Mouhtouris
 * Date: Feb 2nd, 2024
 */

using StoichiometryLibrary;
using System;

namespace Stoichiometry
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //Display Title
            Console.WriteLine("\nStoichiometry 1.0 - L. McCallum, W. Mouhtouris, A. Nicholson\n");

            //Handle Arguments
            if (args.Length == 0)
            {
                Console.WriteLine("No arguments were provided. Use /? for usage info\n");
                return;
            }

            //Switch to handle different user arguments
            switch (args[0])
            {
                case "/?":
                    DisplayHelp();
                    break;
                case "/t":
                    DisplayTable();
                    break;
                case string argument when argument.StartsWith("/f:"):
                    ProcessFile(argument.Substring(3));
                    break;
                default:
                    ProcessFormulas(args);
                    break;
            }
        }


        //Displaying the help board
        static void DisplayHelp()
        {
            Console.WriteLine("\tStoichiometry [/?] [formulas] [/t] [/f:filepath]\n");
            Console.WriteLine("\tformulas\tSpecifies one or more white-space delimited molecular formula of which to compute the molecular mass.");
            Console.WriteLine("\t/?\t\tDisplays usage information.");
            Console.WriteLine("\t/t\t\tLists the elements in the periodic table.");
            Console.WriteLine("\t/f:filepath\tComputes the molecular mass for each formula in the file 'filepath'.");
            Console.WriteLine("\tfilepath\tSpecifies a text file containing molecular formulas, one per line.");
        }


        //Displays the table of chemical elements
        static void DisplayTable()
        {
            Console.WriteLine("\n  Atomic #  Symbol  Name                                 Mass  Period   Group");
            Console.WriteLine("  --------  ------  ----                                 ----  ------   -----");

            foreach (var element in PeriodicTable.Elements)
            {
                Console.WriteLine($"  {element.AtomicNumber,-10}{element.Symbol,-8}{element.Name,-28}{element.AtomicMass,13}{element.Period,8}{element.Group,8}");
            }
        }

        //Takes the molecular formula and displays if its valid, the mass, and composition.
        static void ProcessFormulas(string[] args)
        {
            foreach (string formula in args)
            {
                Molecule molecule = new Molecule(formula);

                if(!molecule.Valid)
                {
                    Console.WriteLine($"\t{formula} is NOT valid\n");
                }
                else
                {
                    Console.WriteLine($"\t{formula} has a mass of {molecule.CalcMass()}\n");

                    foreach (var element in molecule.GetComposition())
                    {
                        Console.WriteLine($"\t\t{element.Symbol} ({element.Name})\t\t{element.AtomicMass} x {element.Multiplier} = {element.AtomicMass * element.Multiplier}");
                    }

                    Console.WriteLine();
                }
            }
        }

        //Processes the file
        static void ProcessFile(string path)
        {
            if (!System.IO.File.Exists(path))
            {
                Console.WriteLine($"{path} not found.");
                return;
            }

            string[] formulas = System.IO.File.ReadAllLines(path);
            ProcessFormulas(formulas);
        }
    }
}