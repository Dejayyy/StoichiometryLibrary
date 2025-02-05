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
        // Properties
        public bool Valid { get; private set; }
        public string Formula { get; set; }

        // Default Constructor
        public Molecule()
        {
            Formula = "";
        }

        // One-Arg Constructor
        public Molecule(string formula)
        {
            Formula = formula;
            Valid = ValidateFormula(formula);
        }

        // Method that returns a double that is the total mass of the molecule described by Formula, if valid
        public double CalcMass()
        {
            // Throw Exception if formula is not valid
            if (!Valid) { throw new InvalidOperationException("Formula is Invalid."); }

            IMolecularElement[] composition = GetComposition();
            double total = 0.0;

            // Total it up
            foreach (Element element in composition)
            {
                total += element.AtomicMass * element.Multiplier;
            }

            return total;
        }

        // Method returning an array of IMolecularElement interfaces containing one item for each element found in Formula, if valid
        public IMolecularElement[] GetComposition()
        {
            // Throw Exception if formula is not valid
            if (!Valid) { throw new InvalidOperationException("Formula is Invalid."); }

            return new IMolecularElement[0];
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
