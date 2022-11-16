using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AStar_8puzzle
{
    internal class AStar
    {
        private PriorityQueue _unproccesssedNodes = new PriorityQueue();
        private HashSet<string> _proccessedNodes = new HashSet<string>();
        private int _maxNodes = 0;

        public void FindSolution(Node startNode)
        {
            Checks checking = new Checks();
            Output output = new Output();
            Node current;
            List<Node> solutionPath = new List<Node>();
            int iterations = 0;
            int comparator;
            int cutoffs = 0;
            Stopwatch stopwatch = new Stopwatch();

            stopwatch.Start();
            _unproccesssedNodes.Enqueue(startNode);
            while (_unproccesssedNodes.Count() != 0)
            {
                if (_unproccesssedNodes.Count() > _maxNodes)
                    _maxNodes = _unproccesssedNodes.Count();
                current = _unproccesssedNodes.Pop();    
                if (!_proccessedNodes.Contains(current.FieldToString()))
                {
                    if (checking.IsVictory(current.GetField()))
                    {
                        stopwatch.Stop();
                        output.PrintField(current.GetField());
                        Console.WriteLine("Solution found! Press any key to see its path. " + (current.GetDepth() - 1));
                        Console.WriteLine("Iterations: " + iterations  + ", cutoffs: " + cutoffs + ", nodes generated: " +
                             (_proccessedNodes.Count() + _unproccesssedNodes.Count()) + "\n" +
                               "maximum nodes in memory count: " + (_proccessedNodes.Count() + _unproccesssedNodes.Count()) + 
                               ", elapsed time: " + stopwatch.Elapsed);
                        Console.ReadKey();
                        while (current != null)
                        {
                            solutionPath.Add(current);
                            current = current.Parent;
                        }
                        solutionPath.Reverse();
                        foreach (Node node in solutionPath)
                        {
                            output.PrintField(node.GetField());
                            Console.WriteLine();
                        }
                        return;
                    }

                    //output.PrintField(current.GetField());
                    //Console.WriteLine("\n");
                    _proccessedNodes.Add(current.FieldToString());
                    comparator = _unproccesssedNodes.Count();
                    MakeNewNodes(current);
                    if (comparator == _unproccesssedNodes.Count())
                        cutoffs++;
                    iterations++;
                }
            }
        }

        public void EnqueueNode(Node startNode, Node newnode)
        {
            if (!_proccessedNodes.Contains(newnode.FieldToString()))
            {
                newnode.value = newnode.GetDepth() + GetManhattanDistance(newnode);
                newnode.Parent = startNode;
                _unproccesssedNodes.Enqueue(newnode);
            }
        }

        public void MakeNewNodes(Node startNode) 
        {
            Node newnode;
            int[,] newField;
            int emptyRow = startNode.GetEmptyRow();
            int emptyColumn = startNode.GetEmptyColumn();

            if (startNode.GetEmptyRow() > 0)
            {
                newField = Manual.Swap(startNode.GetField().Clone() as int[,], emptyRow - 1, emptyColumn, emptyRow, emptyColumn).Clone() as int[,];
                newnode = new Node(newField, startNode.GetDepth() + 1, emptyRow - 1, emptyColumn);
                EnqueueNode(startNode, newnode);
            }

            if (startNode.GetEmptyRow() < startNode.GetField().GetLength(0) - 1)
            {
                newField = Manual.Swap(startNode.GetField().Clone() as int[,], emptyRow + 1, emptyColumn, emptyRow, emptyColumn).Clone() as int[,];
                newnode = new Node(newField, startNode.GetDepth() + 1, emptyRow + 1, emptyColumn);
                EnqueueNode(startNode, newnode);
            }

            if (startNode.GetEmptyColumn() > 0)
            {
                newField = Manual.Swap(startNode.GetField().Clone() as int[,], emptyRow, emptyColumn - 1, emptyRow, emptyColumn).Clone() as int[,];
                newnode = new Node(newField, startNode.GetDepth() + 1, emptyRow, emptyColumn - 1);
                EnqueueNode(startNode, newnode);
            }

            if (startNode.GetEmptyColumn() < startNode.GetField().GetLength(1) - 1)
            {
                newField = Manual.Swap(startNode.GetField().Clone() as int[,], emptyRow, emptyColumn + 1, emptyRow, emptyColumn).Clone() as int[,];
                newnode = new Node(newField, startNode.GetDepth() + 1, emptyRow, emptyColumn + 1);
                EnqueueNode(startNode, newnode);
            }
        }

        public int GetManhattanDistance(Node current) 
        {
            int distance = 0;
            int element;
            byte emptyTile = 0;
            int size = current.GetField().GetLength(0);
            for (int i = 0; i < current.GetField().GetLength(0); i++)
            {
                for (int j = 0; j < current.GetField().GetLength(1); j++)
                {
                    element = current.GetField()[i, j];
                    if (element != emptyTile)
                        distance += Math.Abs((element - 1) / size - i) +
                            Math.Abs((element - 1) % size - j);
                }
            }
            return distance;
        }
    }
}
