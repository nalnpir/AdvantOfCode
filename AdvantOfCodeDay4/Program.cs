using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace AdvantOfCodeDay4
{
    public record Passport
    {
        public string BirthYear { get; set; }
        public string IssueYear { get; set; }
        public string ExpirationYear { get; set; }
        public string Height { get; set; }
        public string HairColor { get; set; }
        public string EyeColor { get; set; }
        public string PassportID { get; set; }
        public string CountryID { get; set; }

        public Passport(string input)
        {
            foreach (var keyvalue in input.Split(new char[] { ' ', '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries))
            {
                AsignarValue(keyvalue);
            }
        }

        public void AsignarValue(string keyvalue)
        {
            var splitted = keyvalue.Split(':');

            switch(splitted[0])
            {
                case "byr":
                    BirthYear = splitted[1];
                    break;
                case "iyr":
                    IssueYear = splitted[1];
                    break;
                case "eyr":
                    ExpirationYear = splitted[1];
                    break;
                case "hgt":
                    Height = splitted[1];
                    break;
                case "hcl":
                    HairColor = splitted[1];
                    break;
                case "ecl":
                    EyeColor = splitted[1];
                    break;
                case "pid":
                    PassportID = splitted[1];
                    break;
                case "cid":
                    CountryID = splitted[1];
                    break;
                default:
                    break;
            }
        }

        public bool isValidPassportWithValues()
        {
            try
            {
                if (!this.isValidPassport()) return false;

                int currentValue = Convert.ToInt32(BirthYear);

                if (BirthYear.Length != 4 || !inBetween(1920, 2002, currentValue))
                    return false;

                currentValue = Convert.ToInt32(IssueYear);
                if (IssueYear.Length != 4 || !inBetween(2010, 2020, currentValue))
                    return false;
                
                currentValue = Convert.ToInt32(ExpirationYear);
                if (ExpirationYear.Length != 4 || !inBetween(2020, 2030, currentValue))
                    return false;

                if (Height.IndexOf("cm") != -1)
                {
                    currentValue = Convert.ToInt32(Height.Substring(0, Height.IndexOf("cm")));
                    if (!inBetween(150, 193, currentValue))
                        return false;
                }
                else if (Height.IndexOf("in") != -1)
                {
                    currentValue = Convert.ToInt32(Height.Substring(0, Height.IndexOf("in")));
                    if (!inBetween(59, 76, currentValue))
                        return false;
                }
                else
                {
                    return false;
                }

                Regex regex = new Regex(@"#[0-9a-f]{6}");
                if (!regex.Match(HairColor).Success)
                    return false;

                var supportedEyeColors = new List<string>() { "amb", "blu", "brn", "gry", "grn", "hzl", "oth" };
                if (supportedEyeColors.Where(x => EyeColor.IndexOf(x) != -1).Count() != 1) 
                    return false;

                regex = new Regex(@"[0-9]{9}");
                if (!regex.Match(PassportID).Success || PassportID.Length != 9)
                    return false;

                return true;
            }
            catch (Exception)
            {
                return false;
            }

        }

        public bool inBetween(int start, int end, int value) => value >= start && value <= end;

        public bool isValidPassport()
        {
            return !string.IsNullOrEmpty(BirthYear) &&
                   !string.IsNullOrEmpty(IssueYear) &&
                   !string.IsNullOrEmpty(ExpirationYear) &&
                   !string.IsNullOrEmpty(Height) &&
                   !string.IsNullOrEmpty(HairColor) &&
                   !string.IsNullOrEmpty(EyeColor) &&
                   !string.IsNullOrEmpty(PassportID);
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            string file = "RealInput.txt";
            var input = System.IO.File.ReadAllText($"{Environment.CurrentDirectory}/{file}").Split(new string[] { Environment.NewLine + Environment.NewLine },
                               StringSplitOptions.RemoveEmptyEntries);

            var passports = input.Select(x => new Passport(x));

            Part1(passports);
            Part2(passports);
        }

        public static void Part1(IEnumerable<Passport> passports)
        {
            Console.WriteLine(passports.Where(x => x.isValidPassport()).Count());
        }

        public static void Part2(IEnumerable<Passport> passports)
        {
            Console.WriteLine(passports.Where(x => x.isValidPassportWithValues()).Count());
        }
    }
}

