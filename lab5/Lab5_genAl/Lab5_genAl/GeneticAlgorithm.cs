using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab5_genAl
{
    internal class GeneticAlgorithm
    {
        Random random = new Random();
        public void StartGeneticAlgorithm()
        {
            int numberOfNodes = 300;
            Node[] nodes = new Node[numberOfNodes];
            for (int i = 0; i < numberOfNodes; i++)
            {
                int x = random.Next(5, 150);
                int y = random.Next(5, 150);
                nodes[i] = new Node(x, y);
            }

            int populationSize = 100;
            List<Individual> population = new List<Individual>();
            for (int i = 0; i < populationSize; i++)
            {
                population.Add(new Individual(nodes));
            }

            int maxGenerations = 100;
            double mutationRate = 0.005;
            double crossoverRate = 0.5;
            int localImprovement = 1;

            for (int generation = 0; generation < maxGenerations; generation++)
            {
                List<Individual> matingPool = new List<Individual>();
                int exponent;
                int minExponent = int.MaxValue;
                double examplefit;
                for (int i = 0; i < populationSize; i++)
                {
                    exponent = 0;
                    examplefit = population[0].Fitness;
                    while (examplefit < 1)
                    {
                        examplefit *= 10;
                        exponent++;
                    }
                    if (exponent < minExponent)
                        minExponent = exponent;
                }
                for (int i = 0; i < populationSize; i++)
                {
                    Individual individual = population[i];
                    int n = (int)(individual.Fitness * Math.Pow(10, minExponent));
                    for (int j = 0; j < n; j++)
                        matingPool.Add(individual);
                }

                List<Individual> newPopulation = new List<Individual>();
                for (int i = 0; i < populationSize; i++)
                {
                    if (crossoverRate > random.NextDouble())
                    {
                        int a = random.Next(matingPool.Count);
                        int b = random.Next(matingPool.Count);
                        Individual parentA = matingPool[a];
                        Individual parentB = matingPool[b];
                        Individual child = parentA.Crossover(parentB);
                        newPopulation.Add(child);
                    }
                    else
                    {
                        int a = random.Next(matingPool.Count);
                        Individual parent = matingPool[a];
                        newPopulation.Add(parent);
                    }
                }

                for (int i = 0; i < newPopulation.Count; i++)
                {
                    if (mutationRate > random.NextDouble())
                    {
                        newPopulation[i].Mutate();
                    }
                }
                population = newPopulation;

                population.Sort();
                Individual.LocalSearch(population[0], localImprovement);
                Console.WriteLine("Generation: " + generation + ", Fits shortest Path by: " + population[0].Fitness);
            }

            population.Sort();
            Console.WriteLine("Fits shortest Path by: " + population[0].Fitness);
            Console.WriteLine("Shortest Path: ");
            for (int i = 0; i < population[0].Path.Count; i++)
            {
                Console.Write(population[0].Path[i].ToString() + "->");
            }
            Console.ReadLine();
        }
    }    
}
