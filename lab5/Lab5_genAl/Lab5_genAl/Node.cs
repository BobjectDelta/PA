using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab5_genAl
{
    class Node
    {
        public int X { get; set; }
        public int Y { get; set; }

        public Node(int x, int y)
        {
            X = x;
            Y = y;
        }

        public override string ToString()
        {
            return Convert.ToString(X) + " " + Convert.ToString(Y);
        }
    }
}
