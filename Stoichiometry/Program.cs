using StoichiometryLibrary;

namespace Stoichiometry
{
    internal class Program
    {
        static void Main(string[] args)
        {
            PeriodicTable table = new PeriodicTable();
            IElement[] n1 = table.LoadElements();

            Console.WriteLine(n1[1].Name);

        }
    }
}
