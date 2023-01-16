using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using Lab5_genAl;

namespace Lab5_genAl
{
    internal class Program
    {
        static void Main(string[] args)
        {
            GeneticAlgorithm genAlgorithm = new GeneticAlgorithm();
            genAlgorithm.StartGeneticAlgorithm();
            Console.ReadKey();          
        }     
    }
}

