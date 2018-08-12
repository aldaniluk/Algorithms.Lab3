using System;
using System.Collections.Generic;

namespace Logic.Graph
{
    public class DirectedGraph
    {
        private int[,] matrix;
        private int size;

        public DirectedGraph(int[,] matrix)
        {
            this.matrix = matrix;
            size = matrix.GetLength(0);
        }

        public Tuple<int, List<int>> Dijkstra(int from, int to)
        {
            bool[] isVisited = new bool[size];
            int[] shortestWays = new int[size]; //кратчайшие пути
            List<int>[] wayVertices = new List<int>[size]; //пути вершины

            int vertexWay = 0, vertexIndex = 0, tempWay;

            for (int i = 0; i < size; i++)
            {
                shortestWays[i] = int.MaxValue;
            }

            shortestWays[from] = 0; //начинаем с этой вершины
            //wayVertices[from] = new List<int>();

            do
            {
                vertexWay = int.MaxValue;
                vertexIndex = int.MaxValue;

                for (int i = 0; i < size; i++)
                {
                    if (!isVisited[i] && shortestWays[i] < vertexWay) //смежная вершина с наим весом ребра
                    {
                        vertexWay = shortestWays[i];
                        vertexIndex = i;
                    }
                }

                if (vertexIndex != int.MaxValue) //если такая вершина есть
                {
                    for (int i = 0; i < size; i++)
                    {
                        if (matrix[vertexIndex, i] > 0) //смотрим смежные вершины
                        {
                            tempWay = vertexWay + matrix[vertexIndex, i]; //пересчитываем путь до найденных вершин
                            if (tempWay < shortestWays[i]) //записываем в shortestWays меньший
                            {
                                shortestWays[i] = tempWay;

                                if (wayVertices[i] == null && wayVertices[vertexIndex] == null) //для каждой вершины строим кратчайший путь
                                {
                                    wayVertices[i] = new List<int>();
                                    wayVertices[i].Add(from);
                                    wayVertices[i].Add(i);
                                }
                                else if (wayVertices[i] == null && wayVertices[vertexIndex] != null)
                                {
                                    wayVertices[i] = new List<int>(wayVertices[vertexIndex]);
                                    wayVertices[i].Add(i);
                                }
                                else
                                {
                                    wayVertices[i] = new List<int>(wayVertices[vertexIndex]);
                                    wayVertices[i].Add(i);
                                }
                            }
                        }
                    }

                    isVisited[vertexIndex] = true;
                }
            } while (vertexIndex < int.MaxValue); //цикл, пока есть смежные вершины

            return Tuple.Create<int, List<int>>(shortestWays[to], wayVertices[to]);
        }


    }
}
