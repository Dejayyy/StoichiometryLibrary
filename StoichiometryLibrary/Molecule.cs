/**
 * Program: Molecule.cs
 * Author: Logan McCallum, Ayden Nicholson, William Mouhtouris
 * Date: Feb 2nd, 2024
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace StoichiometryLibrary
{
    public class Molecule
    {
        //Properties
        public bool Valid { get; private set; }
        public string Formula { get; set; }
        private int Position = 0;   //Position in the formula
        private int SubPosition = 0;

        //Default Constructor
        public Molecule()
        {
            Formula = "";
        }

        //One-Arg Constructor
        public Molecule(string formula)
        {
            Formula = formula;
            Valid = ValidateFormula(formula);
        }

        //Method that returns a double that is the total mass of the molecule described by Formula, if valid
        public double CalcMass()
        {
            //Throw Exception if formula is not valid
            if (!Valid) { throw new InvalidOperationException("Formula is Invalid."); }

            IMolecularElement[] composition = GetComposition();
            double total = 0.0;

            //Total it up
            foreach (Element element in composition)
            {
                total += element.AtomicMass * element.Multiplier;
            }

            return total;
        }

        //Method returning an array of IMolecularElement interfaces containing one item for each element found in Formula, if valid
        public IMolecularElement[] GetComposition()
        {
            Position = 0;

            //Throw Exception if formula is not valid
            if (!Valid) { throw new InvalidOperationException("Formula is Invalid."); }


            Dictionary<string, int> elements = new Dictionary<string, int>();
            

            //Loop through the formula
            while (Position < Formula.Length)
            {
                //Going to need bracket handling here
                if (Formula[Position] == '(')
                {
                    //Get the starting and ending points of the scope
                    int start = Position + 1;
                    int end = Formula.IndexOf(')', start);

                    //Get the subformula
                    string subFormula = Formula.Substring(start, end - start);
                    Position = end + 1;

                    //Check for multiplier to entire subformula
                    int multiplier = ParseMultiplier();

                    var subformulaElements = ParseSubFormula(subFormula);

                    //Multiply subformula elements by multiplier
                    foreach (var element in subformulaElements)
                    {
                        if (elements.ContainsKey(element.Key))
                            elements[element.Key] += element.Value * multiplier;
                        else
                            elements.Add(element.Key, element.Value * multiplier);
                    }
                }
                else
                {
                    //Parse the element
                    string element = ParseElement();

                    //Check for a modifier
                    int multiplier = ParseMultiplier();

                    //Check if element exists in the dictionary
                    if (elements.ContainsKey(element))
                        elements[element] += multiplier;
                    else
                        elements.Add(element, multiplier);
                }
            }

            // Use a list for now (dynamic) then convert to an array
            List<IMolecularElement> composition = new List<IMolecularElement>();

            // Add each element to the array
            foreach (var element in elements)
            {
                // Find element in PeriodicTable
                IElement elementData = PeriodicTable.Elements.FirstOrDefault(e => e.Symbol == element.Key);

                // Turn elementData into an Element
                Element newElement = (Element)elementData;

                // Add the multiplier to it
                Element multiplierAdded = newElement.AddMultiplier((ushort)element.Value);

                // Add it to the composition list
                composition.Add(multiplierAdded);
            }

            // Convert list to an array and return
            return composition.ToArray();
        }

        // Parsing a single element
        private string ParseElement()
        {
            string el = Formula[Position].ToString();
            Position++;

            // Loop until not a lowercase letter
            while (Position < Formula.Length && char.IsLower(Formula[Position]))
            {
                el += Formula[Position];
                Position++;
            }

            return el;
        }

        // Override with two parameters (for handling subformulas)
        private string ParseElement(string formula)
        {
            string el = formula[SubPosition].ToString();
            SubPosition++;

            // Loop until not a lowercase letter
            while (SubPosition < formula.Length && char.IsLower(formula[SubPosition]))
            {
                el += formula[SubPosition];
                SubPosition++;
            }

            return el;
        }

        // Parsing a multiplier (number)
        private int ParseMultiplier()
        {
            int number = 0;

            // loop until no longer number
            while (Position < Formula.Length && char.IsDigit(Formula[Position]))
            {
                // Moves prior digit up, puts current digit into 1s column - minus '0' to get around unicode
                number = number * 10 + (Formula[Position] - '0');
                Position++;
            }

            // if no number found, default modifier is 1 (since the element is present)
            return number == 0 ? 1 : number;
        }

        // Parsing a multiplier (number), overriden for subformula handling (two params)
        private int ParseMultiplier(string formula)
        {
            int number = 0;

            // loop until no longer number
            while (SubPosition < formula.Length && char.IsDigit(formula[SubPosition]))
            {
                // Moves prior digit up, puts current digit into 1s column - minus '0' to get around unicode
                number = number * 10 + (formula[SubPosition] - '0');
                SubPosition++;
            }

            // if no number found, default modifier is 1 (since the element is present)
            return number == 0 ? 1 : number;
        }

        // Parse a subformula
        private Dictionary<string, int> ParseSubFormula(string subformula)
        {
            SubPosition = 0;

            Dictionary<string, int> elements = new Dictionary<string, int>();

            while (SubPosition < subformula.Length)
            {
                string element = ParseElement(subformula);

                int multiplier = ParseMultiplier(subformula);

                if (elements.ContainsKey(element))
                    elements[element] += multiplier;
                else
                    elements.Add(element, multiplier);
            }

            return elements;
        }

        // Addition: Handles formula validation
        private bool ValidateFormula(string formula)
        {
            // First Rule
            if (string.IsNullOrWhiteSpace(formula)) return false;

            // Second Rule
            string regexPattern = @"^((?:[A-Z][a-z]?\d*|\([A-Z][a-z]?\d*(?:[A-Z][a-z]?\d*)*\)\d*)+)$";
            if (!Regex.IsMatch(formula, regexPattern)) return false;

            // Third Rule
            if (char.IsDigit(formula[0])) return false;

            // Fourth Rule
            int count = 0;

            // Makes sure each opening bracket has a closing
            foreach (char c in formula)
            {
                if (c == '(') count++;
                if (c == ')') count--;

                if (c < 0) return false;
            }

            if (count != 0) return false;

            // Fifth Rule
            for (int i = 0; i < formula.Length; i++)
            {
                // if its a digit
                if (char.IsDigit(formula[i]))
                {
                    // make sure its a letter or closing bracket before it
                    if (i == 0 || !(char.IsLetter(formula[i - 1]) || formula[i - 1] == ')')) return false;
                }
            }

            // If all pass, then return true
            return true;
        }
    }
}
