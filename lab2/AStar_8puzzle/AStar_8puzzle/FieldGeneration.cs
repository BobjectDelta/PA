using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AStar_8puzzle
{
    class FieldGeneration
    {
        private int _problemSize = 3;
        public int[,] GenerateAuto()
        {
            int[,] field = new int[_problemSize, _problemSize];
            int[] duplicatePreventor;
            Checks checking = new Checks();
            Random rand = new Random();
            int buff = rand.Next(0, _problemSize * _problemSize);
            do
            {
                duplicatePreventor = new int[_problemSize * _problemSize];
                for (int i = 0; i < duplicatePreventor.Length; i++)
                {
                    duplicatePreventor[i] = -1;
                }
                for (int i = 0; i < duplicatePreventor.Length; i++)
                {
                    while (duplicatePreventor.Contains(buff))
                    {
                        buff = rand.Next(0, _problemSize * _problemSize);
                    }
                    duplicatePreventor[i] = buff;
                }
                for (int i = 0; i < field.GetLength(0); i++)
                {
                    for (int j = 0; j < field.GetLength(1); j++)
                    {
                        field[i, j] = duplicatePreventor[i * field.GetLength(0) + j];
                    }
                }
            }
            while (!checking.IsSolvable(field));
            return field;
        }


        public int[,] GenerateManually()
        {
            int[,] field = new int[_problemSize, _problemSize];
            int[] elementsChecker = new int[_problemSize * _problemSize];
            bool isParced;
            bool isSolvable = true;
            Checks checking = new Checks();
            do
            {
                for (int i = 0; i < elementsChecker.Length; i++)
                    elementsChecker[i] = i;
                if (!isSolvable)
                    Console.WriteLine("This field is not solvable. Try again");
                Console.WriteLine("Enter your field manually:");
                for (int i = 0; i < field.GetLength(0); i++)
                {
                    for (int j = 0; j < field.GetLength(1); j++)
                    {
                        Console.Write(String.Format("Element [{0}][{1}]: ", i, j));
                        isParced = int.TryParse(Console.ReadLine(), out int num);
                        while (!isParced || !elementsChecker.Contains(num) || num == -1)
                        {
                            Console.WriteLine("Error. Try again.");
                            Console.Write(String.Format("Element [{0}][{1}]: ", i, j));
                            isParced = int.TryParse(Console.ReadLine(), out num);
                        }
                        field[i, j] = num;
                        for (int g = 0; g < elementsChecker.Length; g++)
                        {
                            if (elementsChecker[g] == num)
                            {
                                elementsChecker[g] = -1;
                            }
                        }
                    }
                }
                isSolvable = checking.IsSolvable(field);
            }
            while (!isSolvable);
            return field;
        }
    }
}
