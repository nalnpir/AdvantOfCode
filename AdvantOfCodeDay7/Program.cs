using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace AdvantOfCodeDay7
{
    class Bag
    {
        public string BagName { get; set; }
        public Dictionary<Bag, int> Children { get; set; } = new Dictionary<Bag, int>();
        public bool ContainsShinnyBag { get; set; } = false;
        public bool Finished { get; set; } = false;

        public bool HasShinnyBag()
        {
            if (Finished)
                return ContainsShinnyBag;

            if (Children.Count > 0)
                ContainsShinnyBag = Children.Keys.Any(x => x.HasShinnyBag());

            Finished = true;

            return ContainsShinnyBag;
        }

        public long CountChildrenBags()
        {
            long sum = 0;
            foreach (var item in Children.Keys)
            {
                sum += (long)Children[item] + ((long)Children[item] * item.CountChildrenBags());
            }
            return sum;
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            string file = "RealInput.txt";
            var input = System.IO.File.ReadAllText($"{Environment.CurrentDirectory}/{file}").Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries);
            var bags = new List<Bag>();

            foreach (var line in input)
            {
                var result = line.ToLower()
                    .Replace("bags", "")
                    .Replace("bag", "")
                    .Replace(".", "")
                    .Replace("no other", "!!")
                    .Split(new string[] { ",", "contain" }, StringSplitOptions.RemoveEmptyEntries);

                var currentParsedBag = result[0].Trim();

                var bagExists = bags.FirstOrDefault(x => x.BagName.Contains(currentParsedBag));

                var currentBag = bagExists ?? new Bag() { BagName = currentParsedBag };
                
                if (currentParsedBag == "shiny gold")
                {
                    currentBag.ContainsShinnyBag = true;
                    currentBag.Finished = true;
                }

                for (int i = 1; i < result.Length; i++)
                {
                    string parsedName = result[i].Trim();

                    if (parsedName == "!!")
                        break;

                    var numAlpha = new Regex("(?<Numeric>[0-9]*)(?<Alpha>[a-z ]*)");
                    var match = numAlpha.Match(parsedName);

                    string numberPart = match.Groups["Numeric"].Value.Trim();
                    string alphaPart = match.Groups["Alpha"].Value.Trim();

                    var tempBagExists = bags.FirstOrDefault(x => x.BagName.Contains(alphaPart));
                    
                    if (tempBagExists == null)
                    {
                        tempBagExists = new Bag() { BagName = alphaPart, ContainsShinnyBag = alphaPart == "shiny gold" };
                        bags.Add(tempBagExists);
                    }

                    currentBag.Children[tempBagExists] = Convert.ToInt32(numberPart);
                }

                if (bagExists is null)
                {
                    bags.Add(currentBag);
                }
            }

            Part1(bags);
            Part2(bags, bags.First(x => x.BagName == "shiny gold"));
        }

        private static void Part1(List<Bag> bags)
        {
            Console.WriteLine(bags.Where(x => x.HasShinnyBag()).Count() - 1);
        }
        private static void Part2(List<Bag> bags, Bag initialBag)
        {
            Console.WriteLine(initialBag.CountChildrenBags());
        }
    }
}
