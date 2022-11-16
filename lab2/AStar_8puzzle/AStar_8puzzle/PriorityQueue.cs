using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AStar_8puzzle
{
    internal class PriorityQueue
    {
        private List<Node> _list = new List<Node>();

        public PriorityQueue()
        {

        }
        public void Enqueue(Node node)
        {
            _list.Add(node);
        }

        public Node Pop()
        {
            Node mostPrior = _list.First();
            foreach (Node node in _list)
            {
                if (mostPrior > node)
                    mostPrior = node;
            }
            _list.Remove(mostPrior);
            return mostPrior;
        }

        public int Count()
        {
            return _list.Count;
        }
    }
}
