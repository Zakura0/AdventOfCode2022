using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode22
{
    public class Day1
    {
        public Day1(string input)
        {
            Input = input;
        }
        public List<int> Calories { get; set; } = new List<int>();
        public int Part1_Sum => Calories.Max();
        public string Input { get; set; }
        public int Part2_Sum => Calories.OrderByDescending(c => c).Take(3).Sum();

        public void Process()
        {
            var calories = 0;
            var lines = Input.Split('\n');
            Array.Resize(ref lines, lines.Length - 1);
            foreach (var line in lines)
            {
                if (string.IsNullOrWhiteSpace(line))
                {
                    Calories.Add(calories);
                    calories = 0;
                }
                else
                    calories += int.Parse(line);
            }
            Calories.Add(calories);
        }
    }
}
