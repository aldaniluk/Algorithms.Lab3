using Logic.Graph;
using Logic.Labyrinth;
using System;
using System.Collections.Generic;

namespace ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            #region Graphs
            int[,] arr1 = new int[,]
            {
                {0,1,0,0,0,0,0,0},
                {1,0,1,1,0,0,0,0},
                {0,1,0,0,0,0,0,0},
                {0,1,0,0,1,1,1,0},
                {0,0,0,1,0,0,0,0},
                {0,0,0,1,0,0,0,0},
                {0,0,0,1,0,0,0,1},
                {0,0,0,0,0,0,1,0}
            };
            Graph g1 = new Graph(arr1); //connected (without cycles)
            //Console.WriteLine(g1);

            int[,] arr2 = new int[,]
            {
                {0,1,0,0,0,0,0,0},
                {1,0,1,1,0,0,0,0},
                {0,1,0,0,0,0,0,0},
                {0,1,0,0,1,1,1,0},
                {0,0,0,1,0,0,0,1},
                {0,0,0,1,0,0,0,0},
                {0,0,0,1,0,0,0,1},
                {0,0,0,0,1,0,1,0}
            };
            Graph g2 = new Graph(arr2); //connected (with cycles)

            int[,] arr3 = new int[,]
            {
                {0,1,0,0,0,0,0,0},
                {1,0,1,1,0,0,0,0},
                {0,1,0,0,0,0,0,0},
                {0,1,0,0,1,1,1,0},
                {0,0,0,1,0,0,0,0},
                {0,0,0,1,0,0,0,0},
                {0,0,0,1,0,0,0,0},
                {0,0,0,0,0,0,0,0}
            };
            Graph g3 = new Graph(arr3); //not connected

            int[,] arr4 = new int[,]
            {
                {0,1,0,1,0,1,1},
                {1,0,1,0,0,0,0},
                {0,1,0,1,0,0,0},
                {1,0,1,0,1,1,0},
                {0,0,0,1,0,1,0},
                {1,0,0,1,1,0,1},
                {1,0,0,0,0,1,0}
            };
            Graph g4 = new Graph(arr4); //euler!
            #endregion

            #region Connectivity
            Console.WriteLine("\n---------------Connectivity------------");
            Console.WriteLine(g1.IsConnected()); //true
            Console.WriteLine(g2.IsConnected()); //true
            Console.WriteLine(g3.IsConnected()); //false
            #endregion

            #region Euler cycle
            Console.WriteLine("\n-----------EulerianCriterion-----------");
            Console.WriteLine(g1.EulerianCriterion()); //false
            Console.WriteLine(g2.EulerianCriterion()); //false
            Console.WriteLine(g3.EulerianCriterion()); //false
            Console.WriteLine(g4.EulerianCriterion()); //true
            foreach (var i in g4.EulerCyclic())
            {
                Console.Write($"{i} ");
            }
            Console.WriteLine();
            #endregion

            #region Dijkstra
            Console.WriteLine("\n----------------Dijkstra--------------");
            int[,] dirarr1 = new int[,]
            {
                {0,1,0,0,0,7,5},
                {0,0,2,3,0,0,0},
                {0,0,0,0,5,0,0},
                {0,0,0,0,0,0,0},
                {0,0,0,1,0,3,0},
                {0,0,4,0,0,0,0},
                {0,0,0,0,1,0,0}
            };
            DirectedGraph dirgr1 = new DirectedGraph(dirarr1);
            Tuple<int, List<int>> resultDirGr1 = dirgr1.Dijkstra(0, 4);
            Console.Write($"Way length is: {resultDirGr1.Item1}, vertices are: ");
            foreach (var i in resultDirGr1.Item2)
            {
                Console.Write($"{i} ");
            }
            Console.WriteLine();
            #endregion

            #region Labyrinth
            Console.WriteLine("\n----------------Labyrinth--------------");
            int[][] matr = new int[9][]
            {
                new int[] {1,1,1,1,1,1,1,1,1},
                new int[] {0,0,1,1,0,0,1,0,1},
                new int[] {1,0,0,0,1,1,0,0,1},
                new int[] {1,0,0,0,1,1,1,1,1},
                new int[] {1,0,0,0,0,0,0,1,1},
                new int[] {1,0,0,1,0,1,1,0,1},
                new int[] {1,1,1,1,0,0,0,1,1},
                new int[] {1,0,0,1,1,0,0,0,0},
                new int[] {1,1,1,1,1,1,1,1,1}
            };
            Labyrinth lab1 = new Labyrinth(11);

            Console.WriteLine();
            Console.WriteLine(lab1);
            
            if (lab1.FindWay())
            {
                Console.WriteLine(lab1);
            }
            else
            {
                Console.WriteLine("Sorry, there isn't a way :(");
            }

            #endregion
            //Console.ReadLine();

        }
    }
}
