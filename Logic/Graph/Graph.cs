using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Logic.Graph
{
    public class Graph
    {
        private int size;
        private int[,] matrix;

        public Graph(int size)
        {
            this.size = size;
            matrix = new int[size, size];
        }

        public Graph(int[,] matrix)
        {
            size = matrix.GetLength(0);
            this.matrix = matrix;
        }

        #region Connectivity
        //Breadth-first search - поиск в ширину
        public bool IsConnected()
        {
            bool[] visited = new bool[size];
            Queue<int> queue = new Queue<int>();
            queue.Enqueue(0);

            while (queue.Count > 0)
            {
                int v = queue.Dequeue();
                visited[v] = true;

                for (int i = 0; i < size; i++)
                {
                    if (matrix[v, i] == 1 && !visited[i])
                    {
                        queue.Enqueue(i);
                    }
                }
            }

            return !visited.Contains(false);
        }
        #endregion

        #region EulerCycle
        public List<int> EulerCyclic()
        {
            List<int> result = new List<int>();
            Dictionary<int, List<int>> edges = GetEdges();

            if (!EulerianCriterion())
            {
                return result;
            }

            Stack<int> vertices = new Stack<int>();

            vertices.Push(0);

            while (vertices.Count != 0)
            {
                int vertex = vertices.Peek();
                if (edges[vertex].Count == 0) //если все смежные ребра пройдены
                {
                    result.Add(vertex);
                    vertices.Pop();
                }
                else //есть ещё непройденные ребра
                {
                    int newVertex = edges[vertex].First();

                    edges[vertex].Remove(newVertex); //удаляем ребро - мы его прошли
                    edges[newVertex].Remove(vertex); 

                    vertices.Push(newVertex); 
                }
            }

            return result;
        }

        public bool EulerianCriterion()
        {
            for (int i = 0; i < size; i++)
            {
                if (Deg(i) % 2 == 1)
                {
                    return false;
                }
            }

            #region
            //bool[] visited = new bool[size];
            //for (int i = 0; i < size; i++)
            //{
            //    if (Deg(i) > 0)
            //    {
            //        DFS(visited, i);
            //        break;
            //    }
            //}

            //for (int i = 0; i < size; i++)
            //{
            //    if (Deg(i) > 0 && !visited[i])
            //    {
            //        return false;
            //    }
            //}
            #endregion

            return true;
        }
        #endregion

        private int Deg(int v)
        {
            int edges = 0;
            for (int j = 0; j < size; j++)
            {
                if (matrix[v, j] == 1)
                {
                    edges++;
                }
            }

            return edges;
        }

        private Dictionary<int, List<int>> GetEdges()
        {
            Dictionary<int, List<int>> result = new Dictionary<int, List<int>>();
            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    if (matrix[i, j] == 1)
                    {
                        if (result.ContainsKey(i))
                        {
                            result[i].Add(j);
                        }
                        else
                        {
                            result.Add(i, new List<int>() { j });
                        }
                    }
                }
            }

            return result;
        }

        public override string ToString()
        {
            StringBuilder result = new StringBuilder();
            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    result.Append($"{matrix[i, j]} ");
                }

                result.Append('\n');
            }

            return result.ToString();
        }





        #region связный ли граф
        //public bool IsConnected()
        //{
        //    bool isConnected = false;
        //    for (int i = 0; i < size; i++)
        //    {
        //        for (int j = 0; j < size; j++)
        //        {
        //            if (matrix[i, j])
        //            {
        //                isConnected = true;
        //                break;
        //            }
        //        }

        //        if (!isConnected)
        //        {
        //            return false;
        //        }

        //        isConnected = false;
        //    }

        //    return true;
        //}
        #endregion

        #region dfs
        public bool IsCyclic()
        {
            bool[] visited = new bool[size];
            return DFS(visited, 0);
        }

        //Depth-first search ~ поиск в глубину
        private bool DFS(bool[] visited, int i)
        {
            if (visited[i])
            {
                return true;
            }

            visited[i] = true;
            for (int j = i + 1; j < size; j++)
            {
                if (matrix[i, j] == 1 && DFS(visited, j))
                {
                    return true;
                }
            }

            return false;
        }
        #endregion

    }
}
