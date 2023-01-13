using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Salesman_ant_algorithm
{
    class Node
    {
        public int num;
        public double[] edges = new double[200];
        public double[] pheromones = new double[200];
    }
}
