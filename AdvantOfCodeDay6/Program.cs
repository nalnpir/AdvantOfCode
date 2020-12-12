using System;
using System.Collections.Generic;
using System.Linq;

namespace AdvantOfCodeDay6
{
    public record Group
    {
        private string parsedInput;
        private Dictionary<char, int> letterCount = new Dictionary<char, int>();
        private int peopleQuantity;

        public Group(string input)
        {
            var peopleAnswers = input.Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries);
            peopleQuantity = peopleAnswers.Count();

            parsedInput = peopleAnswers.Aggregate((x, y) => $"{x}{y}");
            var chars = parsedInput.ToCharArray();
            foreach (var c in chars)
            {
                if (!letterCount.ContainsKey(c))
                {
                    letterCount[c] = 1;
                }
                else
                {
                    letterCount[c] += 1;
                }
            }
        }

        public int GetUniqueYesAnswers()
        {
            return letterCount.Keys.Count();
        }

        public int GetAllYesAnswers()
        {
            return letterCount.Where(x => x.Value == peopleQuantity).Count();
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            string file = "RealInput.txt";
            var input = System.IO.File.ReadAllText($"{Environment.CurrentDirectory}/{file}").Split(new string[] { Environment.NewLine + Environment.NewLine },
                               StringSplitOptions.RemoveEmptyEntries);

            var groups = input.Select(x => new Group(x));

            Part1(groups);
            Part2(groups);
        }

        private static void Part1(IEnumerable<Group> groups)
        {
            Console.WriteLine(groups.Sum(x => x.GetUniqueYesAnswers()));
        }

        private static void Part2(IEnumerable<Group> groups)
        {
            Console.WriteLine(groups.Sum(x => x.GetAllYesAnswers()));
        }
    }
}
