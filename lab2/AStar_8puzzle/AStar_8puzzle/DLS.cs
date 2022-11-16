using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AStar_8puzzle
{
    class DLS
    {
        private Stack<Node> _unprocessedNodes = new Stack<Node>();
        private int _limit = 31;
        private int _nodesGenerated = 0;
        private int _maxNodes = 0;


        public void FindSolution(Node startNode)
        {
            Checks checking = new Checks();
            Output output = new Output();
            Node current;
            List<Node> solutionPath = new List<Node>();
            int cutoffs = 0;
            int iterations = 0;
            Stopwatch stopwatch = new Stopwatch();

            stopwatch.Start();
            _unprocessedNodes.Push(startNode);

            while(_unprocessedNodes.Count() != 0)
            {
                if (_unprocessedNodes.Count() > _maxNodes)
                    _maxNodes = _unprocessedNodes.Count();
                current = _unprocessedNodes.Pop();
                if(checking.IsVictory(current.GetField()))
                {
                    stopwatch.Stop();
                    output.PrintField(current.GetField());
                    Console.WriteLine("Solution found! Press any key to see its path. " + (current.GetDepth()-1));
                    Console.WriteLine("Iterations: " + iterations + ", cutoffs: " + cutoffs + ", nodes generated: " + _nodesGenerated
                        + "\n" +"maximum nodes in memory count: " + _maxNodes + ", elapsed time: " + stopwatch.Elapsed);
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
                if (current.GetDepth() > _limit)               
                    cutoffs++;   
                else
                {
                    MakeNewNodes(current);
                    iterations++;
                }
            }
            Console.WriteLine("Failed to find a solution");
        }

        public void PushNode(Node startNode, Node newnode)
        {
            if (startNode.Parent == null)
            {
                newnode.Parent = startNode;
                _unprocessedNodes.Push(newnode);
                _nodesGenerated++;
                return;
            }
            else if (newnode.FieldToString() != startNode.Parent.FieldToString())
            {
                newnode.Parent = startNode;
                _unprocessedNodes.Push(newnode);
                _nodesGenerated++;
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
                PushNode(startNode, newnode);
            }

            if (startNode.GetEmptyColumn() > 0)
            {
                newField = Manual.Swap(startNode.GetField().Clone() as int[,], emptyRow, emptyColumn - 1, emptyRow, emptyColumn).Clone() as int[,];
                newnode = new Node(newField, startNode.GetDepth() + 1, emptyRow, emptyColumn - 1);
                PushNode(startNode, newnode);
            }

            if (startNode.GetEmptyRow() < startNode.GetField().GetLength(0) - 1)
            {
                newField = Manual.Swap(startNode.GetField().Clone() as int[,], emptyRow + 1, emptyColumn, emptyRow, emptyColumn).Clone() as int[,];
                newnode = new Node(newField, startNode.GetDepth() + 1, emptyRow + 1, emptyColumn);
                PushNode(startNode, newnode);
            }

            if (startNode.GetEmptyColumn() < startNode.GetField().GetLength(1) - 1)
            {
                newField = Manual.Swap(startNode.GetField().Clone() as int[,], emptyRow, emptyColumn + 1, emptyRow, emptyColumn).Clone() as int[,];
                newnode = new Node(newField, startNode.GetDepth() + 1, emptyRow, emptyColumn + 1);
                PushNode(startNode, newnode);
            }
        }
    }
}
