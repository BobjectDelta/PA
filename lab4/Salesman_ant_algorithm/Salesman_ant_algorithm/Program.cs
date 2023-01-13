using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Salesman_ant_algorithm
{
    internal class Program
    {
        static void Main(string[] args)
        {
            AntColonyAlgorithm colonyAlgorithm = new AntColonyAlgorithm();
            colonyAlgorithm.AntAlgorithm();
            Console.ReadKey();
        }
    }
}
