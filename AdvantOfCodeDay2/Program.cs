using System;
using System.Collections.Generic;
using System.Linq;

namespace AdvantOfCodeDay2
{
    public record Password
    {
        public int FirstNumber { get; init; }
        public int SecondNumber { get; init; }
        public char Letter { get; init; }
        public string OriginalPassword { get; init; }

        public Password(string value)
        {
            var parsed = value.Split(' ', StringSplitOptions.RemoveEmptyEntries);
            var minMax = parsed[0].Split('-', StringSplitOptions.RemoveEmptyEntries).Select(x => Int32.Parse(x)).ToArray();
            FirstNumber = minMax[0];
            SecondNumber = minMax[1];
            Letter = Convert.ToChar(parsed[1].Substring(0, 1));
            OriginalPassword = parsed[2];
        }

        public bool IsStrongEnough()
        {
            Dictionary<char, int> letterAmount = new Dictionary<char, int>();

            foreach (var letter in OriginalPassword.ToCharArray())
            {
                if (letterAmount.ContainsKey(letter))
                    letterAmount[letter] += 1;
                else
                    letterAmount[letter] = 1;
            }


            return letterAmount.ContainsKey(Letter) && letterAmount[Letter] >= FirstNumber && letterAmount[Letter] <= SecondNumber;
        }

        public bool ApprovedRequirement()
        {
            int found = 0;

            found += OriginalPassword.Substring(FirstNumber - 1, 1) == Letter.ToString() ? 1 : 0;
            found += OriginalPassword.Substring(SecondNumber - 1, 1) == Letter.ToString() ? 1 : 0;

            return found == 1;
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            string file = "RealInput.txt";
            var input = System.IO.File.ReadAllText($"{Environment.CurrentDirectory}/{file}").Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries);

            var convertedInput = input.Select(x => new Password(x));

            Part1(convertedInput);
            Part2(convertedInput);
        }

        private static void Part1(IEnumerable<Password> convertedInput)
        {
            Console.WriteLine(convertedInput.Where(x => x.IsStrongEnough()).Count());
        }

        private static void Part2(IEnumerable<Password> convertedInput)
        {
            Console.WriteLine(convertedInput.Where(x => x.ApprovedRequirement()).Count());
        }
    }
}
