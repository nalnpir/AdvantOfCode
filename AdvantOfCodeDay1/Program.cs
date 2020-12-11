using System;
using System.Linq;

namespace AdvantOfCodeDay1
{
    class Program
    {
        static void Main(string[] args)
        {
            string file = "RealInput.txt";
            var input = System.IO.File.ReadAllText($"{Environment.CurrentDirectory}/{file}").Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries);

            var convertedInput = input.Select(x => Convert.ToInt32(x)).ToArray();

            Part1(convertedInput);
            Part2(convertedInput);
        }

        private static void Part1(int[] convertedInput)
        {
            int firstIndex = 0;
            int secondIndex = 1;

            while (convertedInput[firstIndex] + convertedInput[secondIndex] != 2020 || firstIndex == secondIndex)
            {
                if (secondIndex + 1 < convertedInput.Length)
                {
                    secondIndex++;
                }
                else
                {
                    secondIndex = firstIndex + 2;
                    firstIndex++;
                }
            }

            Console.WriteLine(convertedInput[firstIndex] * convertedInput[secondIndex]);
        }

        private static void Part2(int[] convertedInput)
        {
            int firstIndex = 0;
            int secondIndex = 1;
            int thirdIndex = 2;

            while (convertedInput[firstIndex] + convertedInput[secondIndex] + convertedInput[thirdIndex] != 2020)
            {
                if (thirdIndex + 1 < convertedInput.Length)
                {
                    thirdIndex++;
                }
                else if(secondIndex + 1 < convertedInput.Length - 1)
                {
                    thirdIndex = secondIndex + 2;
                    secondIndex++;
                }
                else
                {
                    thirdIndex = firstIndex + 3;
                    secondIndex = firstIndex + 2;
                    firstIndex++;
                }
            }

            Console.WriteLine(convertedInput[firstIndex] * convertedInput[secondIndex] * convertedInput[thirdIndex]);
        }
    }
}
