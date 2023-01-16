using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab5_genAl
{
    class Individual : IComparable<Individual>
    {
        Random random = new Random();
        public List<Node> Path { get; set; }
        public double Fitness { get; set; }

        public Individual(Node[] nodes)
        {
            Path = new List<Node>();
            List<Node> remainingNodes = new List<Node>(nodes);

            Node startNode = remainingNodes[random.Next(remainingNodes.Count)];
            Path.Add(startNode);
            remainingNodes.Remove(startNode);
            while (remainingNodes.Count > 0)
            {
                Node nextNode = remainingNodes[random.Next(remainingNodes.Count)];
                Path.Add(nextNode);
                remainingNodes.Remove(nextNode);
            }

            Fitness = CalculateFitness();
        }

        public Individual(List<Node> path)
        {
            Path = new List<Node>(path);
            Fitness = CalculateFitness();
        }

        public void Mutate()
        {
            int a = random.Next(Path.Count);
            int b = random.Next(Path.Count);
            Node temp = Path[a];
            Path[a] = Path[b];
            Path[b] = temp;

            Fitness = CalculateFitness();
        }

        public Individual Crossover(Individual other)
        {
            int crossoverPoint = random.Next(Path.Count);

            List<Node> newPath = new List<Node>();
            for (int i = 0; i < crossoverPoint; i++)
            {
                newPath.Add(Path[i]);
            }
            for (int i = 0; i < other.Path.Count; i++)
            {
                if (!newPath.Contains(other.Path[i]))
                {
                    newPath.Add(other.Path[i]);
                }
            }
            return new Individual(newPath);
        }

        public double CalculateFitness()
        {
            double totalDistance = 0;
            for (int i = 0; i < Path.Count - 1; i++)
            {
                Node a = Path[i];
                Node b = Path[i + 1];
                double distance = Math.Sqrt(Math.Pow(b.X - a.X, 2) + Math.Pow(b.Y - a.Y, 2));
                totalDistance += distance;
            }
            return 1 / totalDistance;
        }

        public static void LocalSearch(Individual individual, int improvementRate)
        {
            bool improvement = true;
            int g = 0;
            while (improvement && g <= improvementRate)
            {
                improvement = false;
                for (int i = 0; i < individual.Path.Count - 2; i++)
                {
                    for (int j = i + 2; j < individual.Path.Count - 2; j++)
                    {
                        double originalDistance = Distance(individual.Path[i], individual.Path[i + 1]) + Distance(individual.Path[j], individual.Path[j + 1]);
                        double newDistance = Distance(individual.Path[i], individual.Path[j]) + Distance(individual.Path[i + 1], individual.Path[j + 1]);
                        if (newDistance < originalDistance)
                        {
                            individual.Path.Reverse(i + 1, j - i - 1);
                            individual.Fitness = individual.CalculateFitness();
                            improvement = true;
                        }
                    }
                }
                g++;
            }
        }

        private static double Distance(Node a, Node b)
        {
            return Math.Sqrt(Math.Pow(b.X - a.X, 2) + Math.Pow(b.Y - a.Y, 2));
        }
        public int CompareTo(Individual other)
        {
            return Fitness.CompareTo(other.Fitness);
        }
    }
}
