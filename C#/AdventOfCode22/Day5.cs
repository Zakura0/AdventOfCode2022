using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode22
{
    public class Day5
    {
        public string Input { get; set; }
        public Day5(string input)
        {
            Input = input;
        }

        public void Process()
        {
            var lines = Input.Split('\n');
            Array.Resize(ref lines, lines.Length - 1);
            string[] stacks = new string[9];
            for (int i = 0; i < 8; i++)
            {
                int position = 0;
                for (int j = 1; j < 36; j += 4)
                {
                    if ((lines[i])[j] != ' ')
                    {
                        stacks[(j - 1) / 4] = stacks[(j - 1) / 4] + (lines[i])[j].ToString();
                    }
                }

            }
            lines = lines.Skip(10).ToArray();
            foreach (var line in lines)
            {
                var cut_line = "";
                cut_line = line.Replace("move ", "");
                cut_line = cut_line.Replace("from ", "");
                cut_line = cut_line.Replace("to ", "");
                var index = cut_line.Split(' ');
                var character = (stacks[Int32.Parse(index[1]) - 1]).Substring(0, Int32.Parse(index[0]));
                stacks[Int32.Parse(index[1]) - 1] = stacks[Int32.Parse(index[1]) - 1].Substring(Int32.Parse(index[0]));
                stacks[Int32.Parse(index[2]) - 1] = character + stacks[Int32.Parse(index[2]) - 1];
            }
            foreach (var line in stacks)
            {
                Console.Write(line[0]);
            }
        }
    }
}
