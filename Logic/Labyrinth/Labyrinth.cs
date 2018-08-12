using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Logic.Labyrinth
{
    public class Labyrinth
    {
        private int[][] matrix;
        private int size;

        private int START_X;
        private int START_Y;
        private int END_X;
        private int END_Y;

        //private static int EXIT_INDEX = 2;

        private List<Tuple<int, int>> totalWay;

        public Labyrinth(int[][] matrix)
        {
            this.size = matrix[0].Length;
            this.matrix = matrix;

            START_X = 1;
            START_Y = 0;
            END_X = size - 2;
            END_Y = size - 1;
        }

        public Labyrinth(int size)
        {
            this.size = size;

            START_X = 1;
            START_Y = 0;
            END_X = size - 2;
            END_Y = size - 1;


            matrix = new int[size][];
            GenerateWithDFS();
        }

        private void GenerateWithDFS()
        {
            bool[][] visited = new bool[size][];
            for (int i = 0; i < size; i++)
            {
                visited[i] = new bool[size];
            }

            Generate();

            Stack<Tuple<int, int>> stack = new Stack<Tuple<int, int>>();
            Tuple<int, int> currCell = Tuple.Create<int, int>(1, 1);
            visited[0][1] = true;
            stack.Push(currCell);

            Random rand = new Random();

            while (!AllSellsVisited(visited))
            {
                List<Tuple<int, int>> randNeighborCells;
                if (HasUnvisitedNeighbors(visited, currCell, out randNeighborCells))
                {
                    stack.Push(currCell);
                    Tuple<int, int> randNeighborCell = randNeighborCells[rand.Next(0, randNeighborCells.Count)];
                    DeleteWall(currCell, randNeighborCell);
                    currCell = randNeighborCell;
                    visited[currCell.Item1][currCell.Item2] = true;

                }
                else //if (stack.Count != 0)
                {
                    currCell = stack.Pop();
                }
            }
        }

        private void DeleteWall(Tuple<int, int> currCell, Tuple<int, int> randNeighborCell)
        {
            if (currCell.Item1 == randNeighborCell.Item1)
            {
                matrix[currCell.Item1][(currCell.Item2 + randNeighborCell.Item2) / 2] = 1;
            }
            else
            {
                matrix[(currCell.Item1 + randNeighborCell.Item1) / 2][currCell.Item2] = 1;
            }
        }

        private bool HasUnvisitedNeighbors(bool[][] visited, Tuple<int, int> cell, out List<Tuple<int, int>> randNeighborCells)
        {
            randNeighborCells = new List<Tuple<int, int>>();
            if (IndexIsCorrect(cell.Item1 + 2) && !visited[cell.Item1 + 2][cell.Item2])
            {
                randNeighborCells.Add(Tuple.Create<int, int>(cell.Item1 + 2, cell.Item2));
            }
            if (IndexIsCorrect(cell.Item1 - 2) && !visited[cell.Item1 - 2][cell.Item2])
            {
                randNeighborCells.Add(Tuple.Create<int, int>(cell.Item1 - 2, cell.Item2));
            }
            if (IndexIsCorrect(cell.Item2 + 2) && !visited[cell.Item1][cell.Item2 + 2])
            {
                randNeighborCells.Add(Tuple.Create<int, int>(cell.Item1, cell.Item2 + 2));
            }
            if (IndexIsCorrect(cell.Item2 - 2) && !visited[cell.Item1][cell.Item2 - 2])
            {
                randNeighborCells.Add(Tuple.Create<int, int>(cell.Item1, cell.Item2 - 2));
            }

            if (randNeighborCells.Count != 0)
            {
                return true;
            }

            return false;
        }

        private bool AllSellsVisited(bool[][] visited)
        {
            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    if (i % 2 == 1 && j % 2 == 1 && !visited[i][j])
                    {
                        return false;
                    }
                }
            }

            return true;
        }

        private void Generate()
        {
            Random rand = new Random();

            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    if (matrix[i] == null)
                    {
                        matrix[i] = new int[size];
                    }

                    if (i == 0 || i == size - 1 || j == 0 || j == size - 1)
                    {
                        matrix[i][j] = 0; //wall along border
                    }
                    else if (i % 2 == 1 && j % 2 == 1)
                    {
                        matrix[i][j] = 1; //cell
                    }
                    else
                    {
                        matrix[i][j] = 0;// rand.Next(0, 2);
                    }
                }
            }

            matrix[START_X][START_Y] = 1;
            matrix[END_X][END_Y] = 1;

            //Console.Write(this);
        }

        public bool FindWay()
        {
            bool[][] visited = new bool[size][];
            for (int i = 0; i < size; i++)
            {
                visited[i] = new bool[size];
            }

            totalWay = new List<Tuple<int, int>>(); //exit way!
            totalWay.Add(Tuple.Create<int, int>(START_X, START_Y));

            return DFS(visited, 1, 0);
        }

        //Depth-first search ~ поиск в глубину
        private bool DFS(bool[][] visited, int i, int j)
        {
            if (visited[i][j])
            {
                return false;
            }

            visited[i][j] = true;
            //matrix[i][j] = EXIT_INDEX;
            totalWay.Add(Tuple.Create<int, int>(i, j));

            if (IndexIsCorrect(j + 1) && matrix[i][j + 1] == 1 && DFS(visited, i, j + 1))
            {
                return true;
            }
            if (IndexIsCorrect(j - 1) && matrix[i][j - 1] == 1 && DFS(visited, i, j - 1))
            {
                return true;
            }
            if (IndexIsCorrect(i + 1) && matrix[i + 1][j] == 1 && DFS(visited, i + 1, j))
            {
                return true;
            }
            if (IndexIsCorrect(i - 1) && matrix[i - 1][j] == 1 && DFS(visited, i - 1, j))
            {
                return true;
            }
            

            if (i == END_X && j == END_Y)
            {
                //matrix[i][j] = EXIT_INDEX;
                //SimplifyWay();
                return true;
            }

            //do
            //{
            totalWay.Remove(totalWay[totalWay.Count - 1]);
            //} while (!visited[totalWay.Peek().Item1][totalWay.Peek().Item2]);
            return false;
        }

        private bool IndexIsCorrect(int i)
        {
            return i >= 0 && i < size;
        }

        private void SimplifyWay()
        {
            for (int i = 0; i < totalWay.Count - 1; i++)
            {
                Tuple<int, int> currTuple = totalWay[i];
                if (totalWay.Where(t => t.Item1 == currTuple.Item1 && t.Item2 == currTuple.Item2 + 1).Count() != 0)
                {

                    while (totalWay[i + 1].Item1 != currTuple.Item1 || totalWay[i + 1].Item2 != currTuple.Item2 + 1)
                    {
                        totalWay.Remove(totalWay[i + 1]);

                        if (totalWay.Count < i + 1)
                        {
                            break;
                        }
                    }

                }
            }
        }

        public override string ToString()
        {
            StringBuilder result = new StringBuilder();
            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    if (totalWay != null && totalWay.Contains(Tuple.Create<int, int>(i, j)))
                    {
                        result.Append("++");
                    }

                    //if (matrix[i][j] == EXIT_INDEX)
                    //{
                    //    result.Append("--");
                    //}
                    else if (matrix[i][j] == 1)
                    {
                        result.Append("  ");
                    }

                    //else if (i == 0 && j == 0)
                    //{
                    //    result.Append("┌");
                    //}
                    //else if (i == 0 && j == size - 1)
                    //{
                    //    result.Append("┐");
                    //}
                    //else if (i == size - 1 && j == 0)
                    //{
                    //    result.Append("└");
                    //}
                    //else if (i == size - 1 && j == size - 1)
                    //{
                    //    result.Append("┘");
                    //}

                    //else if(i == 0 || i == size-1)
                    //{
                    //    result.Append("─");
                    //}
                    //else if (j == 0 || j == size - 1)
                    //{
                    //    result.Append("│");
                    //}

                    //else
                    //{
                    //    result.Append("┼");
                    //}


                    else
                    {
                        char c = '\x2588';
                        result.Append(c.ToString() + c.ToString());
                    }
                }

                result.Append("\n");
            }

            return result.ToString();
        }
    }
}
