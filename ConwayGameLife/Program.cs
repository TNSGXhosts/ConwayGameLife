using System;
using System.Linq;

namespace ConwayGameLife
{
    public class ConwayLife
    {
        public static int[,] GetGeneration(int[,] cells, int generation)
        {
            int[,] nextGeneration = new int[cells.GetLength(0), cells.GetLength(1)];
            int[,] currentGeneration = new int[cells.GetLength(0), cells.GetLength(1)];
            Array.Copy(cells, currentGeneration, cells.Length);
            for (int gen = 0; gen < generation; gen++)
            {
                for (int i = 0; i < currentGeneration.GetLength(0); i++)
                {
                    for (int j = 0; j < currentGeneration.GetLength(1); j++)
                    {
                        if (currentGeneration[i, j] == 0)
                        {
                            int[,] arr = new int[3, 3];
                            int num = FindLife(CellNeighborhood(currentGeneration, i, j));
                            if (num == 3)
                            {
                                nextGeneration[i, j] = 1;
                            }
                            else
                            {
                                nextGeneration[i, j] = currentGeneration[i, j];
                            }
                        }
                        else
                        {
                            int[,] arr = new int[3, 3];
                            int num = FindLife(CellNeighborhood(currentGeneration, i, j));
                            if (num < 2 || num > 3)
                            {
                                nextGeneration[i, j] = 0;
                            }
                            else
                            {
                                nextGeneration[i, j] = currentGeneration[i, j];
                            }
                        }
                    }
                }

                Array.Copy(nextGeneration, currentGeneration, nextGeneration.Length);
            }
            return currentGeneration;
        }


        private static int FindLife(int[,] cells)
        {
            int numberLives = 0;
            for (int i = 0; i < cells.GetLength(0); i++)
            {
                for (int j = 0; j < cells.GetLength(1); j++)
                {
                    if (!(i == 1 && j == 1) && cells[i, j] == 1)
                    {
                        numberLives++;
                    }
                }
            }
            return numberLives;
        }

        private static int[,] CellNeighborhood(int[,] arr, int iC, int jC)
        {
            int[,] cellNeighborhood = new int[3, 3];
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    if (iC - 1 + i < 0 || iC - 1 + i >= arr.GetLength(0) || jC - 1 + j < 0 || jC - 1 + j >= arr.GetLength(1))
                    {
                        cellNeighborhood[i, j] = 0;
                    }
                    else
                    {
                        cellNeighborhood[i, j] = arr[iC - 1 + i, jC - 1 + j];
                    }
                }
            }
            return cellNeighborhood;
        }

        private static int[,] Resize(int[,] cells)  //У тебя рот не закроется это делать
        {
            int[,] output = new int[cells.GetLength(0), cells.GetLength(1)];
            Array.Copy(cells, output, cells.Length);
            int top = GetRow(cells, 0).Sum();
            int bottom = GetRow(cells, cells.GetLength(0)).Sum();
            int left = GetCol(cells, 0).Sum();
            int right = GetCol(cells, cells.GetLength(1)).Sum();
            if (top == 0)
            {
                int[,] newArr = new int[output.GetLength(0) - 1, output.GetLength(1)];
                Array.Copy(output, output.GetLength(1), newArr, 0, output.Length - output.GetLength(1));
                output = newArr;
            }
            if (bottom == 0)
            {
                int[,] newArr = new int[output.GetLength(0) - 1, output.GetLength(1)];
                Array.Copy(output, 0, newArr, output.GetLength(1), output.Length - output.GetLength(1));
                output = newArr;
            }
            if (left == 0)
            {
                output = reduceArrayLeft(output);
            }
            //TODO: Увеличение сетки, если 3 подряд жизни где-то по периметру
            return output;
        }

        private static int[] GetRow(int[,] cells, int num)
        {
            int[] row = new int[cells.GetLength(1)];
            for (int i = 0; i < cells.GetLength(1); i++)
            {
                row[i] = cells[num, i];
            }
            return row;
        }

        private static int[] GetCol(int[,] cells, int num)
        {
            int[] col = new int[cells.GetLength(1)];
            for (int i = 0; i < cells.GetLength(1); i++)
            {
                col[i] = cells[i, num];
            }
            return col;
        }

        private static int[,] reduceArrayLeft(int[,] arr)
        {
            int[,] output = new int[arr.GetLength(0), arr.GetLength(1) - 1];
            for (int i = 1; i < arr.GetLength(0); i++)
            {
                for (int j = 1; i < arr.GetLength(1); j++)
                {
                    output[i - 1, j] = arr[i, j];
                }
            }
            return output;
        }

        private enum Parties
        {
            left = 1,
            right = 0
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            int[,] res = ConwayLife.GetGeneration(new int[,] { { 1, 0, 0 }, { 0, 1, 1 }, { 1, 1, 0 } }, 0);
            for (int i = 0; i < res.GetLength(0); i++)
            {
                for (int j = 0; j < res.GetLength(1); j++)
                {
                    Console.Write(res[i, j] + " ");
                }
                Console.WriteLine();
            }
        }
    }
}
