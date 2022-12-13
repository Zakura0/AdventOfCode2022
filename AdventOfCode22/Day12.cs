using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

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
                    var cell = new Cell(row, col, (byte)lines[row][col] - 96);
                    Grid[row, col] = cell;
                    if (cell.value == -13)
                    {
                        Start = cell;
                    }
                    else if (cell.value == -27)
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
        public void Part1()
        {
            ProcessGrid();
            var test = GetNeighbors(End);
        }
        private static bool IsValidNeighbor(Cell current, Cell neighbor)
        {
            if (current.value == StartValue)
            {
                return neighbor.value <= 2;
            }
            if (neighbor.value == EndValue)
            {
                return current.value == 26;
            }

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
        
    }
    public record Cell(int x, int y, int value);
}
