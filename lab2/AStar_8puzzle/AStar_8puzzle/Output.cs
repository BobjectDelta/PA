using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AStar_8puzzle
{
    class Output
    {
        public void PrintField(int[,] field)
        {
            for (int i = 0; i < field.GetLength(0); i++)
            {
                for (int j = 0; j < field.GetLength(1); j++)
                {
                    Console.Write(String.Format("{0, 3}", field[i, j]));
                }
                Console.WriteLine();
            }
        }
    }
}
