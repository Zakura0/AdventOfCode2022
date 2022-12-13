using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode22
{
    public class Day4
    {
        private string Input { get; }
        public Day4(string input) => Input = input;
        public int Part2_Sum { get; set; }
        public void Part1()
        {
            var lines = Input.Split('\n');
            Array.Resize(ref lines, lines.Length - 1);
            foreach (var line in lines)
            {
                var split = line.Split('-', ',');
                var index1 = Int32.Parse(split[0]);
                var index2 = Int32.Parse(split[1]);
                var index3 = Int32.Parse(split[2]);
                var index4 = Int32.Parse(split[3]);
                List<int> part1 = new List<int>();
                List<int> part2 = new List<int>();
                for (int i = index1; i <= index2; i++)
                {
                    part1.Add(i);
                }
                for (int i = index3; i <= index4; i++)
                {
                    part2.Add(i);
                }
                var temp1 = part1.Except(part2).ToList();
                var temp2 = part2.Except(part1).ToList();
                if (temp1.Count != part1.Count)
                {
                    Part2_Sum++;
                }
                else if (temp2.Count != part2.Count)
                {
                    Part2_Sum++;
                }
            }
        }
    }
}
