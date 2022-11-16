using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AStar_8puzzle
{
    public class Node : IComparable
    {
        private int[,] _field;  
        public int value;       
        private int _emptyRow;
        private int _emptyColumn;
        private int _depth;
        public Node Parent;

        public Node(int[,] field, int depth, int emptyRow, int emptyColumn)
        {
            _field = field.Clone() as int[,];
            _depth = depth;
            _emptyRow = emptyRow;
            _emptyColumn = emptyColumn;
        }

        public Node(int[,] field, int depth)
        {
            _field = field.Clone() as int[,];
            _depth = depth;
            bool isFound = false;
            for (int i = 0; i < _field.GetLength(0) && !isFound; i++)
            {
                for (int j = 0; j < _field.GetLength(1) && !isFound; j++)
                {
                    if (_field[i, j] == 0)
                    {
                        _emptyRow = i;
                        _emptyColumn = j;
                        isFound = true;
                    }
                }
            }
        }

        public Node()
        {
        }

        public int CompareTo(object obj)
        {
            if (obj != null)
            {
                Node otherNode = obj as Node;
                if (otherNode != null)
                    return value.CompareTo(otherNode.value);
                else
                    throw new ArgumentException("Object can't be a Node");
            }
            else
                return 1;
        }

        public string FieldToString()
        {
            string str = "";
            for (int i = 0; i < _field.GetLength(0); i++)
            {
                for (int j = 0; j < _field.GetLength(1); j++)
                    str += Convert.ToString(_field[i, j]) + ',';
            }
            return str;
        }

        public int[,] GetField()
        {
            return _field;
        }
        public int GetEmptyRow()
        {
            return _emptyRow;
        }
        public int GetEmptyColumn()
        {
            return _emptyColumn;
        }
        public int GetDepth()
        {
            return _depth;
        }

        public static bool operator >(Node node1, Node node2)
        {
            return node1.value > node2.value;
        }

        public static bool operator <(Node node1, Node node2)
        {
            return node1.value < node2.value;
        }

    }
}
