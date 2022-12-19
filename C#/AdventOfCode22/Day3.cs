using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode22
{
    public class Day3
    {
        private string Input { get; set; }
        public int Part2_Sum { get; set; }
        public int Part1_Sum { get; set; }
        public Day3(string input) 
        {
            Input = input;
        }

        public void Part2()
        {
            var lines = Input.Split('\n');
            Array.Resize(ref lines, lines.Length - 1);
            var index = 0;
            var line1 = "";
            var line2 = "";
            var line3 = "";
            foreach (var line in lines)
            {
                index++;
                switch (index)
                {
                    case 1:
                        line1 = line;
                        break;
                    case 2:
                        line2 = line;
                        break;
                    case 3:
                        line3 = line;
                        break;
                }
                if (index == 3)
                {
                    for (int i = 0; i < line1.Length; i++)
                    {
                        if (line2.Contains(line1[i]))
                        {
                            if (line3.Contains(line1[i]))
                            {
                                var equals3 = line1[i];
                                if (Char.IsUpper(equals3))
                                {
                                    Part2_Sum = Part2_Sum + (((byte)equals3 - 64) + 26);
                                }
                                else
                                {
                                    Part2_Sum = Part2_Sum + ((byte)equals3 - 96);
                                }
                                break;
                            }
                        }
                    }
                    index = 0;
                }
            }
        }


        public void Part1()
        {
            var lines = Input.Split('\n');
            Array.Resize(ref lines, lines.Length - 1);
            foreach (var line in lines)
            {
                string part1 = line.Substring(0, line.Length / 2);
                string part2 = line.Substring(line.Length / 2, line.Length / 2);
                for (int i = 0; i < part2.Length; i++)
                {
                    if (part2.Contains(part1[i]))
                    {
                        var temp = part1[i];
                        if (Char.IsUpper(temp))
                        {
                            Part1_Sum = Part1_Sum + (((byte)temp - 64) + 26);
                        }
                        else
                        {
                            Part1_Sum = Part1_Sum + ((byte)temp - 96);
                        }
                        break;
                    }
                }
            }
        }
    }
}
