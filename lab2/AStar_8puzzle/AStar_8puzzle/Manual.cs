using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace AStar_8puzzle
{
    internal class Manual
    {
        public static int[,] Swap(int[,] field, int i1, int j1, int i2, int j2)
        {
            int tmp = field[i1, j1];
            field[i1, j1] = field[i2, j2];
            field[i2, j2] = tmp;
            return field;
        }

        public void ManualSolution(Node startNode)
        {
            int emptyRow = 0;
            int emptyColumn = 0;
            string dir = "";
            bool isMoved = false;
            byte emptyTile = 0;
            int[,] field = startNode.GetField().Clone() as int[,];
            Checks checking = new Checks();
            Output output = new Output();
            for (int i = 0; i < field.GetLength(0); i++)
            {
                for (int j = 0; j < field.GetLength(1); j++)
                {
                    if (field[i, j] == emptyTile)
                    {
                        emptyRow = i;
                        emptyColumn = j;
                    }
                }
            }
            while (!checking.IsVictory(field))
            {
                Console.WriteLine("Choose direction (up-1/down-2/left-3/right-4):");
                dir = Console.ReadLine();
                do
                {
                    if (dir == "1" && emptyRow > 0)
                    {
                        field = Swap(field, emptyRow - 1, emptyColumn, emptyRow, emptyColumn).Clone() as int[,];
                        emptyRow--;
                        isMoved = true;
                    }
                    else if (dir == "2" && emptyRow < field.GetLength(0) - 1)
                    {
                        field = Swap(field, emptyRow + 1, emptyColumn, emptyRow, emptyColumn).Clone() as int[,];
                        emptyRow++;
                        isMoved = true;
                    }
                    else if (dir == "3" && emptyColumn > 0)
                    {
                        field = Swap(field, emptyRow, emptyColumn - 1, emptyRow, emptyColumn).Clone() as int[,];
                        emptyColumn--;
                        isMoved = true;
                    }
                    else if (dir == "4" && emptyColumn < field.GetLength(1) - 1)
                    {
                        field = Swap(field, emptyRow, emptyColumn + 1, emptyRow, emptyColumn).Clone() as int[,];
                        emptyColumn++;
                        isMoved = true;
                    }
                    else
                        Console.WriteLine("Impossible move. Try again.");
                    if (isMoved)
                        output.PrintField(field);
                }
                while (!isMoved);
            }
            Console.WriteLine("You win!");
        }
    }
}
