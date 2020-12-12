using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace AdvantOfCodeDay4
{
    public record Ticket
    {
        public string TicketNumber { get; set; }

        public int GetSeatNumber()
        {
            int minValue = 0;
            int maxValue = 127;
            int rowValue = 0;
            int colValue = 0;

            for (int i = 0; i < TicketNumber.Substring(0, 7).Length; i++)
            {
                if (TicketNumber.Substring(i, 1) == "F")
                {
                    maxValue = (maxValue + minValue) / 2;
                    rowValue = maxValue;
                }
                else
                {
                    minValue = (maxValue + minValue) / 2 + 1;
                    rowValue = minValue;
                }

            }

            minValue = 0;
            maxValue = 7;

            for (int i = 0; i < TicketNumber.Substring(7, 3).Length; i++)
            {
                if (TicketNumber.Substring(7+i, 1) == "L")
                {
                    maxValue = (maxValue + minValue) / 2;
                    colValue = maxValue;
                }
                else
                {
                    minValue = (maxValue + minValue) / 2 + 1;
                    colValue = minValue;
                }

            }

            return (rowValue * 8) + colValue;
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            string file = "RealInput.txt";
            var input = System.IO.File.ReadAllText($"{Environment.CurrentDirectory}/{file}").Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries);

            var tickets = input.Select(x => new Ticket { TicketNumber = x });

            Part1(tickets);
            Part2(tickets);
        }

        public static void Part1(IEnumerable<Ticket> tickets)
        {
            Console.WriteLine(tickets.Max(x => x.GetSeatNumber()));
        }

        public static void Part2(IEnumerable<Ticket> tickets)
        {
            var orderedSeatNumbers = tickets.Select(x => x.GetSeatNumber()).ToList().OrderBy(x => x).ToArray();

            //we start index 1, we dont go to the last value since the number must be in between 2 numbers
            for (int i = 1; i < orderedSeatNumbers.Count() - 2; i++)
            {
                if (orderedSeatNumbers[i] + 1 != orderedSeatNumbers[i+1])
                {
                    Console.WriteLine(orderedSeatNumbers[i] + 1);
                    break; //number is unique so we can abort
                }
            }

        }
    }
}

