using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Salesman_ant_algorithm
{
    class Ant
    {
        public List<Node> proccessedNodes = new List<Node>();
        public List<Node> unproccessedNodes = new List<Node>();
        public Node position;
        public int pheromone = 5;
        public Ant(Node startingPosition, List<Node> allNodes)
        {
            position = startingPosition;
            proccessedNodes.Add(startingPosition);
            unproccessedNodes = CopyList(allNodes);
            unproccessedNodes.Remove(position);
        }

        public void Move(int A, int B)
        {
            List<double> probs = new List<double>();

            List<double> nums = new List<double>();
            for (int i = 0; i < unproccessedNodes.Count; i++)
            {
                double num = Math.Pow(proccessedNodes[proccessedNodes.Count - 1].pheromones[unproccessedNodes[i].num], A) * 
                    Math.Pow(1 / proccessedNodes[proccessedNodes.Count - 1].edges[unproccessedNodes[i].num], B);
                nums.Add(num);
            }
            for (int i = 0; i < nums.Count; i++)           
                probs.Add(nums[i] / Sum(nums));           
            Random rnd = new Random();
            double random = rnd.NextDouble();
            for (int i = 0; i < probs.Count; i++)
            {
                if (random < Sum(probs.GetRange(0, i + 1)))
                {
                    position = unproccessedNodes[i];
                    unproccessedNodes.Remove(position);
                    proccessedNodes.Add(position);
                    i = 1000;
                }
            }
        }
        public double GetLength()
        {
            double length = 0;
            for (int i = 0; i < proccessedNodes.Count - 1; i++)
                length += proccessedNodes[i].edges[proccessedNodes[i + 1].num];
            length += proccessedNodes[proccessedNodes.Count - 1].edges[proccessedNodes[0].num];

            return length;
        }

        public double Sum(List<double> nums)
        {
            double sum = 0;
            foreach (var num in nums)
                sum += num;

            return sum;
        }
        public void LeavePheromone()
        {
            double l = GetLength();
            for (int i = 0; i < proccessedNodes.Count - 1; i++)
            {
                proccessedNodes[i].pheromones[proccessedNodes[i + 1].num] += AntColonyAlgorithm.Lmin / l;
                proccessedNodes[i + 1].pheromones[proccessedNodes[i].num] = proccessedNodes[i].pheromones[proccessedNodes[i + 1].num];
            }
            proccessedNodes[proccessedNodes.Count - 1].pheromones[proccessedNodes[0].num] += AntColonyAlgorithm.Lmin / l;
            proccessedNodes[0].pheromones[proccessedNodes[proccessedNodes.Count - 1].num] = 
                proccessedNodes[proccessedNodes.Count - 1].pheromones[proccessedNodes[0].num];
        }

        public List<Node> CopyList(List<Node> baseList)
        {
            List<Node> newList = new List<Node>();
            foreach (var element in baseList)          
                newList.Add(element);           
            return newList;
        }
    }
}

