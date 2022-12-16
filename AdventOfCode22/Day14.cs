using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace AdventOfCode22
{
    public class Day14
    {
        public int Part1_Sum { get; set; }
        public int Part2_Sum { get; set; }
        private string Input { get; set; }
        private int minx { get; set; }
        private int maxx { get; set; }
        private int miny { get; set; }
        private int maxy { get; set; }
        private CaveCell[,] _Cave { get; set; }
        public Day14(string input)
        {
            Input = input;
            //Input = "498,4 -> 498,6 -> 496,6\n503,4 -> 502,4 -> 502,9 -> 494,9";
        }
        public void Process()
        {
            _Cave = ProcessCave(false);
            Part1_Sum = DropSand(_Cave);
            _Cave = ProcessCave(true);
            Part2_Sum = DropSand(_Cave);

        }
        private int DropSand(CaveCell[,] _cave)
        {
            var sum = 0;
            var thevoid = _cave.GetLength(0);
            var sandy = 0;
            var sandx = 500 - minx + 200;
            bool free = true;
            bool inVoid = false;
            while (free && !inVoid)
            {
                if (sandy == thevoid - 1)
                {
                    inVoid = true;
                }
                else if (_cave[sandy + 1, sandx].identifier == '.')
                {
                    if (_cave[sandy, sandx].identifier == '+')
                    {
                        _cave[sandy, sandx] = new CaveCell(sandy, sandx, '.');
                    }
                    _cave[sandy + 1, sandx] = new CaveCell(sandy + 1, sandx, '+');
                    sandy++;
                }
                else if (_cave[sandy + 1, sandx - 1].identifier == '.')
                {
                    if (_cave[sandy, sandx].identifier == '+')
                    {
                        _cave[sandy, sandx] = new CaveCell(sandy, sandx, '.');
                    }
                    _cave[sandy + 1, sandx - 1] = new CaveCell(sandy + 1, sandx - 1, '+');
                    sandy++;
                    sandx--;
                }
                else if (_cave[sandy + 1, sandx + 1].identifier == '.')
                {
                    if (_cave[sandy, sandx].identifier == '+')
                    {
                        _cave[sandy, sandx] = new CaveCell(sandy, sandx, '.');
                    }
                    _cave[sandy + 1, sandx + 1] = new CaveCell(sandy + 1, sandx + 1, '+');
                    sandy++;
                    sandx++;
                }
                else if (sandy == 0)
                {
                    _cave[0, 500 - minx + 200] = new CaveCell(0, 500 - minx + 200, '+');
                    sum++;
                    free = false;
                }
                else
                {
                    _cave[sandy, sandx] = new CaveCell(sandy, sandx, '+');
                    sum++;
                    sandy = 0;
                    sandx = 500 - minx + 200;
                }
            }
            return sum;
            
        }
        private void PrintCave(CaveCell[,] input)
        {
            for (int i = 0; i < input.GetLength(0); i++)
            {
                Console.WriteLine();
                for (int j = 0; j < input.GetLength(1); j++)
                {
                    Console.Write(input[i, j].identifier);
                }
            }
        }
        private CaveCell[,] ProcessCave(bool build_floor)
        {
            var lines = Input.Split('\n');
            Array.Resize(ref lines, lines.Length - 1);
            List<int> rows = new List<int>();
            List<int> cols = new List<int>();
            foreach (var line in lines)
            {
                var str = line.Split(" -> ");
                foreach (var num in str)
                {
                    rows.Add(int.Parse(num.Split(',')[0]));
                    cols.Add(int.Parse(num.Split(',')[1]));
                }
            }
            minx = rows.Min();
            maxx = rows.Max();
            maxy = cols.Max();
            var cave_sizex = maxx - minx;
            CaveCell[,] Cave = new CaveCell[maxy + 3, cave_sizex + 400];
            for (int row = 0; row < Cave.GetLength(0); row++)
            {
                for (int col = 0; col < Cave.GetLength(1); col++)
                {
                    if (build_floor && row == maxy + 2)
                    {
                        Cave[row, col] = new CaveCell(row, col, '#');
                    }
                    else
                    {
                        Cave[row, col] = new CaveCell(row, col, '.');
                    }
                }
            }
            foreach (var line in lines)
            {
                var rocks = line.Split(" -> ");
                var start = true;
                int ax = 0;
                int ay = 0;
                int bx = 0;
                int by = 0;
                var lastRock = rocks[0];
                foreach (var rock in rocks)
                {
                    if (!start)
                    {
                        ax = int.Parse(lastRock.Split(',')[0]);
                        ay = int.Parse(lastRock.Split(',')[1]);
                        bx = int.Parse(rock.Split(',')[0]);
                        by = int.Parse(rock.Split(',')[1]);
                        lastRock = rock;
                        if (ax == bx)
                        {
                            if (ay < by)
                            {
                                for (int j = ay; j <= by; j++)
                                {
                                    Cave[j - miny, ax - minx + 200] = new CaveCell(j, ax, '#');
                                }
                            }
                            else
                            {
                                for (int j = by; j <= ay; j++)
                                {
                                    Cave[j - miny, ax - minx + 200] = new CaveCell(j, ax, '#');
                                }
                            }

                        }
                        else
                        {
                            if (ax < bx)
                            {
                                for (int j = ax; j <= bx; j++)
                                {
                                    Cave[ay - miny, j - minx+ 200] = new CaveCell(ay, j, '#');
                                }
                            }
                            else
                            {
                                for (int j = bx; j <= ax; j++)
                                {
                                    Cave[ay - miny, j - minx + 200] = new CaveCell(ay, j, '#');
                                }
                            }
                        }
                    }
                    start = false;
                }
            }
            return Cave;
        }
        public record CaveCell(int y, int x, char identifier);
    }
}
