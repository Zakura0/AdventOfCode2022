using System;
using System.Collections;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace AdventOfCode22
{
    public class Day12
    {
        public string Input { get; set; }
        public Cell[,] Grid { get; set; }
        private Cell Start { get; set; }
        private Cell End { get; set; }
        private int Rows { get; set; }
        private int Columns { get; set; }
        private const int StartValue = -13;
        private const int EndValue = -27;
        public int Part1_Sum { get; set; }
        public int Part2_Sum { get; set; }

        private Dictionary<Cell, List<Cell>> Neighbors = new();
        public Day12(string input)
        {
            Input = input;
        }
        void ProcessGrid()
        {
            var lines = Input.Split('\n');
            Array.Resize(ref lines, lines.Length - 1);
            Rows = lines.Count();
            Columns = lines[0].Length;
            Grid = new Cell[Rows, Columns];
            for (int row = 0; row < Rows; row++)
            {
                for (int col = 0; col < Columns; col++)
                {
                    Cell cell;
                    if ((byte)lines[row][col] - 96 == -13)
                    {
                        cell = new Cell(row, col, 1);
                    }
                    else if ((byte)lines[row][col] - 96 == -27)
                    {
                        cell = new Cell(row, col, 26);
                    }
                    else
                    {
                        cell = new Cell(row, col, (byte)lines[row][col] - 96);
                    }
                    Grid[row, col] = cell;
                    if ((byte)lines[row][col] - 96 == -13)
                    {
                        Start = cell;
                    }
                    else if ((byte)lines[row][col] - 96 == -27)
                    {
                        End = cell;
                    }
                }
            }
            for (int row = 0; row < Rows; row++)
            {
                for (int col = 0; col < Columns; col++)
                {
                    var cell = Grid[row, col];
                    var leftNeighbor = GetCellIfInBounds(row, col - 1);
                    var rightNeighbor = GetCellIfInBounds(row, col + 1);
                    var upNeighbor = GetCellIfInBounds(row - 1, col);
                    var downNeighbor = GetCellIfInBounds(row + 1, col);
                    var neighbors = new List<Cell?>
                    {
                        leftNeighbor, rightNeighbor, upNeighbor, downNeighbor
                    };
                    Neighbors[cell] = neighbors.Where(neighbor => neighbor != null && IsValidNeighbor(cell, neighbor)).Select(neighbor => neighbor!).ToList();
                }
            }
        }
        private List<Cell> GetNeighbors(Cell cell) => Neighbors[cell];
        public void Process()
        {
            ProcessGrid();
            Part1_Sum = BreadthFirstSearch(Start, End);
            List<Cell> p2list = new List<Cell>();
            for (int row = 0; row < Grid.GetLength(0); row++)
            {
                for (int col = 0; col < Grid.GetLength(1); col++)
                {
                    if (Grid[row, col].value == 1)
                    {
                        p2list.Add(Grid[row, col]);
                    }
                }
            }
            List<int> paths = new List<int>();
            foreach (var cell in p2list)
            {
                paths.Add(BreadthFirstSearch(cell, End));
            }
            Part2_Sum = paths.Min();
        }
        private int BreadthFirstSearch(Cell StartPos, Cell End)
        {
            var queue1 = new Queue()!;
            var queue2 = new Queue()!;
            queue1.Enqueue(StartPos);
            var steps = 0;
            var seen_cells = new HashSet<Cell>();
            while (!seen_cells.Contains(End))
            {
                steps++;
                if (queue1.Count == 0)
                {
                    return 100000;
                }
                while (queue1.Count != 0)
                {
                    var current = (Cell)queue1.Dequeue();
                    foreach (var neighbor in GetNeighbors(current))
                    {
                        if (!seen_cells.Contains(neighbor) && !queue2.Contains(neighbor))
                        {
                            queue2.Enqueue(neighbor);
                            seen_cells.Add(neighbor);
                        }
                    }
                }
                queue1 = (Queue)queue2.Clone();
                queue2.Clear();
            }
            return steps;
        }
        private static bool IsValidNeighbor(Cell current, Cell neighbor)
        {
            return neighbor.value == StartValue || neighbor.value - current.value <= 1;
        }
        private Cell? GetCellIfInBounds(int row, int col)
        {
            if (row >= 0 && row < Rows && col >= 0 && col < Columns)
            {
                return Grid[row, col];
            }
            else return null;
        }

        private static List<Cell> BuildPath(Cell start, Cell end, IReadOnlyDictionary<Cell, Cell?> cameFrom)
        {
            if (!cameFrom.ContainsKey(end))
            {
                return new List<Cell>();
            }

            var pathCell = end;
            var path = new List<Cell>();

            while (pathCell != start)
            {
                path.Add(pathCell);
                pathCell = cameFrom[pathCell]!;
            }
            path.Reverse();

            return path;
        }
    }
    public record Cell(int x, int y, int value);
}
