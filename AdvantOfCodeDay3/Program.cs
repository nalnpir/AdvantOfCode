using System;

namespace AdvantOfCodeDay3
{
    class Program
    {
        static void Main(string[] args)
        {
            string file = "RealInput.txt";
            var input = System.IO.File.ReadAllText($"{Environment.CurrentDirectory}/{file}").Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries);

            var matrix = new char[input.Length, input[0].Length];
            int i = 0;
            foreach (var line in input)
            {
                int j = 0;
                foreach (var c in line.ToCharArray())
                {
                    matrix[i, j] = c;
                    j++;
                }
                i++;
            }

            //Part 1
            Console.WriteLine(CalculateEncounteredTrees(matrix,3, 1));

            //Part 2
            long resultado = CalculateEncounteredTrees(matrix, 1, 1);
            resultado *= CalculateEncounteredTrees(matrix, 3, 1);
            resultado *= CalculateEncounteredTrees(matrix, 5, 1);
            resultado *= CalculateEncounteredTrees(matrix, 7, 1);
            resultado *= CalculateEncounteredTrees(matrix, 1, 2);
            Console.WriteLine(resultado);
            //Part2(convertedInput);
        }

        private static int CalculateEncounteredTrees(char[,] matrix, int right, int down)
        {
            int amountOfCols = matrix.GetLength(1) - 1;
            int amountOfRows = matrix.GetLength(0) - 1;
            int colIndex = 0, rowIndex = 0, amountOfTrees = 0;

            while (amountOfRows > rowIndex)
            {
                colIndex += right;
                rowIndex += down;

                if (colIndex > amountOfCols)
                    colIndex -= (amountOfCols + 1);

                if (matrix[rowIndex, colIndex] == '#')
                    amountOfTrees++;
            }

            return amountOfTrees;
        }
    }
}
