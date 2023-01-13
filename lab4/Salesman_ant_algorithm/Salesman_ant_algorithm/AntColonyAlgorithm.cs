using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Salesman_ant_algorithm
{
    class AntColonyAlgorithm
    {
        public int alpha = 3;
        public int beta = 2;
        public float rho = 0.7f;
        public const int edgeNumber = 200;
        public int edgeMin = 1;
        public int edgeMax = 40;
        public int antQty = 45;
        public static double Lmin;
        private Node[] nodes = new Node[edgeNumber];

        public void AntAlgorithm()
        {
            PlaceNodes();
            Lmin = SeekLMin();
            Random rand = new Random();
            List<Ant> ants = new List<Ant>();

            for (int i = 0; i < 100; i++)
            {
                ants.Clear();
                for (int j = 0; j < antQty; j++)                              
                    ants.Add(new Ant(nodes[rand.Next(0, edgeNumber-1)], nodes.ToList()));                

                for (int j = 0; j < edgeNumber-1; j++)
                {
                    for (int k = 0; k < ants.Count; k++)                    
                        ants[k].Move(alpha, beta);                   
                }
                for (int j = 0; j < edgeNumber; j++)
                {
                    for (int k = 0; k < edgeNumber - 1; k++)                    
                        nodes[j].pheromones[k] *= 1 - rho;                   
                }
                for (int k = 0; k < ants.Count; k++)             
                    ants[k].LeavePheromone();               
                for (int k = 0; k < ants.Count; k++)                
                    ants[k].Move(alpha, beta);

                if (i%20 == 0)
                {
                    double curTrail = 0;
                    double minTrail = double.MaxValue;
                    for (int j = 0; j < ants.Count; j++)
                    {
                        curTrail = ants[j].GetLength();
                        if (curTrail < minTrail)
                            minTrail = curTrail;                       
                    }
                    Console.WriteLine(i + "," + minTrail);
                }
            }

            for (int i = 0; i < ants.Count; i++)
            {
                string path = "";
                for (int j = 0; j < ants[i].proccessedNodes.Count; j++)               
                    path += ants[i].proccessedNodes[j].num.ToString() + " - ";                
                Console.WriteLine(path + "\n");
            }        
        }

        public void PlaceNodes()
        {
            Random rnd = new Random();
            for (int i = 0; i < edgeNumber; i++)
            {
                nodes[i] = new Node();
                nodes[i].num = i;
            }
            for (int i = 0; i < edgeNumber; i++)
            {
                for (int j = 0; j < edgeNumber; j++)
                {
                    nodes[i].pheromones[j] = 0.5f;
                    if (nodes[i].num == j)
                        nodes[i].edges[j] = 0;
                    else if (nodes[j].edges[i] == 0)
                    {
                        float number = rnd.Next(edgeMin, edgeMax);
                        nodes[i].edges[j] = number;
                        nodes[j].edges[i] = number;
                    }
                    else if (nodes[j].edges[i] != 0)
                        nodes[i].edges[j] = nodes[j].edges[i];
                }
            }
        }

        public double SeekLMin()
        {
            Ant ant = new Ant(nodes[0], nodes.ToList());

            for (int i = 0; i < edgeNumber - 1; i++)          
                ant.Move(0, 1);          
            return ant.GetLength();
        }
    }
}

