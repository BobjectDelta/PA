using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace AStar_8puzzle
{
    internal class Program
    {
        static void Main(string[] args)
        {
            FieldGeneration fieldGeneration = new FieldGeneration();
            Output output = new Output();
            Manual manualGame = new Manual();
            Checks checking = new Checks();
            AStar aStar = new AStar();
            DLS DLS = new DLS();
            Node node = new Node();
            string mode;

            do
            {
                Console.WriteLine("Choose generation mode: 0-Auto, 1-Manual");
                mode = Console.ReadLine();
            }
            while (!mode.Equals("0") && !mode.Equals("1"));
            if (mode == "0")
                node = new Node(fieldGeneration.GenerateAuto(), 0);
            else
                node = new Node(fieldGeneration.GenerateManually(), 0);
            output.PrintField(node.GetField());
            Console.ReadKey();

            do
            {
                Console.WriteLine("Choose mode: 0-AStar, 1-LDFS");
                mode = Console.ReadLine();
            }
            while (!mode.Equals("0") && !mode.Equals("1"));
            if (mode == "0")
                aStar.FindSolution(node);
            else
                DLS.FindSolution(node);

            Console.ReadKey();
        }
    }
}
