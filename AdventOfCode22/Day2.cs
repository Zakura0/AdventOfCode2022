using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode22
{
    public class Day2
    {
        private string Input { get; set; }
        public int Day2_Sum { get; set; }
        public Day2(string input) 
        {
            Input = input;
        }
        public void Process()
        {
            var lines = Input.Split('\n');
            Array.Resize(ref lines, lines.Length - 1);
            foreach (var line in lines)
            {
                var game = line.Replace(" ", "");
                switch (game)
                {
                    case "AX":
                        Day2_Sum += 3;
                        break;
                    case "AY":
                        Day2_Sum += 4;
                        break;
                    case "AZ":
                        Day2_Sum += 8;
                        break;
                    case "BX":
                        Day2_Sum += 1;
                        break;
                    case "BY":
                        Day2_Sum += 5;
                        break;
                    case "BZ":
                        Day2_Sum += 9;
                        break;
                    case "CX":
                        Day2_Sum += 2;
                        break;
                    case "CY":
                        Day2_Sum += 6;
                        break;
                    case "CZ":
                        Day2_Sum += 7;
                        break;
                }
            }
        }
    }
}
