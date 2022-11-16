using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AStar_8puzzle
{
    class Checks
    {
        public int[,] victoryField = new int[3, 3] { {1, 2, 3}, {4, 5, 6},
                                                    {7, 8, 0}};
        public bool IsVictory(int[,] field)
        {
            for (int i = 0; i < field.GetLength(0); i++)
            {
                for (int j = 0; j < field.GetLength(1); j++)
                {
                    if (field[i, j] != victoryField[i, j])
                        return false;
                }
            }
            return true;
        }


        public bool IsSolvable(int[,] field)
        {
            int count = 0;
            int emptyRow = 0;
            int emptyTile = 0;
            int[] array = field.Cast<int>().ToArray();
            for (int i = 0; i < array.Length; i++)
            {
                for (int j = i; j < array.Length; j++)
                {
                    if (array[i] > array[j] && array[j] != emptyTile)
                        count++;
                    if (array[i] == emptyTile)
                        emptyRow = i / field.GetLength(0) + 1;
                }
            }
            if ((count /*+ emptyRow*/) % 2 == 0)
                return true;
            else
                return false;
        }
    }
}
