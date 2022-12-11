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
        record struct Knot(int row, int col);
        private string Input { get; set; }
        public int Part1_Sum { get; set; }
        public int Part2_Sum { get; set; }
        public Day9(string input)
        {
            this.Input = input;
        }
        private IEnumerable<Knot> Knots(string input, int ropeLength)
        {
            var lines = Input.Split('\n');
            Array.Resize(ref lines, lines.Length - 1);
            var knots = Enumerable.Repeat(new Knot(0, 0), ropeLength).ToArray();
            yield return knots.Last();

            foreach (var line in lines)
            {
                var parts = line.Split(' ');
                var direction = parts[0];
                var amount = int.Parse(parts[1]);

                for (var i = 0; i < amount; i++)
                {
                    MoveHead(knots, direction);
                    yield return knots.Last();
                }
            }
        }

        public void Part1()
        {
            Part1_Sum = Knots(Input, 2).ToHashSet().Count;
        }
        public void Part2()
        {
            Part2_Sum = Knots(Input, 10).ToHashSet().Count;
        }
        private void MoveHead(Knot[] rope, string dir)
        {
            rope[0] = dir switch
            {
                "U" => rope[0] with
                {
                    row = rope[0].row - 1 
                },
                "D" => rope[0] with 
                {
                    row = rope[0].row + 1
                },
                "L" => rope[0] with 
                {
                    col = rope[0].col - 1
                },
                "R" => rope[0] with
                {
                    col = rope[0].col + 1
                }
            };
            //move tails
            for (var i = 1; i < rope.Length; i++)
            {
                var trow = rope[i - 1].row - rope[i].row;
                var tcol = rope[i - 1].col - rope[i].col;

                if (Math.Abs(trow) > 1 || Math.Abs(tcol) > 1)
                {
                    rope[i] = new Knot(
                        rope[i].row + Math.Sign(trow),
                        rope[i].col + Math.Sign(tcol)
                    );
                }
            }
        }

    }
}
