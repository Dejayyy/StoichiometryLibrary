using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoichiometryLibrary
{
    public class Molecule
    {
        // Properties
        public bool Valid { get; }
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
        }

        // Method that returns a double that is the total mass of the molecule described by Formula, if valid
        public double CalcMass()
        {
            // Throw Exception if formula is not valid
            if (!Valid) { throw new InvalidOperationException("Formula is Invalid."); }

            return 0.0;
        }

        // Method returning an array of IMolecularElement interfaces containing one item for each element found in Formula, if valid
        public IMolecularElement[] GetComposition()
        {
            // Throw Exception if formula is not valid
            if (!Valid) { throw new InvalidOperationException("Formula is Invalid."); }

            return new IMolecularElement[0];
        }
    }
}
