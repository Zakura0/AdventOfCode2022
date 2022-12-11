using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode22
{
    public class Day9
    {
        private struct Knot
        {
            public int row;
            public int col;
        }
        private string Input { get; set; }
        public Day9(string input)
        {
            this.Input = input;
        }
        public void Part1()
        {
            Knot Head = new Knot();
            Knot Tail = new Knot();
            Head.row = 0;
            Head.row = 0;
            Tail.row = 0;
            Tail.row = 0;
            var lines = Input.Split('\n');
            Array.Resize(ref lines, lines.Length - 1);
            foreach (var line in lines)
            {
                var parts = line.Split(' ');
                var amount = int.Parse(parts[1]);
                var direction = parts[0];
                for (var i = 0; i < amount; i++)
                {
                    MoveHead(Head, direction);
                }
                
            }
        }

        private void MoveHead(Knot head, string dir)
        {
            switch (dir)
            {
                case "U":
                    head.row += 1;
                    break;
                case "D":
                    head.row -= 1;
                    break;
                case "R":
                    head.col += 1;
                    break;
                case "L":
                    head.col -= 1;
                    break;
            }
        }

    }
}
