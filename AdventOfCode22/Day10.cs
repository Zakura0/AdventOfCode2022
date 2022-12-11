using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode22
{
    public class Day10
    {
        private string Input { get; set; }
        public string Part1_Sum { get; set; }

        public Day10(string input)
        {
            this.Input = input;
        }
        public void Part1()
        {
            var sample = new[] { 20, 60, 100, 140, 180, 220 };
            int i = Signal(Input)
                .Where(signal => sample.Contains(signal.cycle))
                .Select(signal => signal.x * signal.cycle)
                .Sum();
            Part1_Sum = i.ToString();

        }
        public void Part2()
        {
            var test = Signal(Input).Select(signal =>
            {
                var spriteMiddle = signal.x;
                var col = (signal.cycle - 1) % 40;
                return Math.Abs(spriteMiddle - col) < 2 ? '#' : '.';
            }).ToArray();
            for (int i = 0; i < test.Count(); i++)
            {
                Console.Write(test[i]);
                if (i % 40 == 0)
                {
                    Console.Write("\n");
                }
            }
        }
        IEnumerable<(int cycle, int x)> Signal(string input)
        {
            var (cycle, x) = (1, 1);
            var lines = input.Split("\n");
            Array.Resize(ref lines, lines.Length - 1);
            foreach (var line in lines)
            {
                var parts = line.Split(" ");
                switch (parts[0])
                {
                    case "addx":
                        yield return (cycle++, x);
                        yield return (cycle++, x);
                        x += int.Parse(parts[1]);
                        break;
                    default:
                        yield return (cycle++, x);
                        break;
                }
            }
        }
    }
}
