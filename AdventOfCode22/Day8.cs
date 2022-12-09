using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode22
{
    public class Day8
    {
        public string Input { get; set; }
        public Day8(string input)
        {
            this.Input = input;
            ProcessTrees();
        }
        public int[,] Trees { get; set; }
        public int SizeOfTreeLine { get; set; } = 0;
        public long Part1_Sum { get; set; }
        public long Part2_Score { get; set; }
        public void ProcessTrees()
        {
            var lines = Input.Split('\n');
            Array.Resize(ref lines, lines.Length - 1);
            int rows = 0;
            SizeOfTreeLine = lines[0].Length;
            foreach (var line in lines)
            {
                if (Trees == null)
                {
                    Trees = new int[line.Length, line.Length];
                }
                for (int couloums = 0; couloums < line.Length; couloums++)
                {
                    Trees[rows, couloums] = int.Parse(line.Substring(couloums, 1));
                }
                rows++;
            }            
        }
        public void Part1()
        {
            for (int j = 0; j < SizeOfTreeLine; j++)
            {
                for (int i = 0; i < SizeOfTreeLine; i++)
                {
                    if (j == 1 && i == 5)
                    {

                    }
                    if (visibleFromLeft(j, i) || visibleFromRight(j, i) || visibleFromTop(j, i) || visibleFromBottom(j, i))
                    {
                        Part1_Sum++;
                    }
                }
            }
        }
        public void Part2()
        {
            var maxScore = 0;
            var score = 0;
            for (int j = 0; j < SizeOfTreeLine; j++)
            {
                for (int i = 0; i < SizeOfTreeLine; i++)
                {
                    score = distanceToBottom(j, i) * distanceToLeft(j, i) * distanceToRight(j, i) * distanceToTop(j, i);
                    maxScore = Math.Max(score, maxScore);
                }
            }
            Part2_Score = maxScore;
        }
        private bool visibleFromLeft(int row, int col)
        {
            var tree = Trees[row, col];
            for (int i = col - 1; i >= 0; i--)
            {
                if (Trees[row, i] >= tree)
                {
                    return false;
                }
            }
            return true;
        }
        private bool visibleFromRight(int row, int col)
        {
            var tree = Trees[row, col];
            for (int i = col + 1; i < Trees.GetLength(0); i++)
            {
                if (Trees[row, i] >= tree)
                {
                    return false;
                }
            }
            return true;
        }
        private bool visibleFromTop(int row, int col)
        {
            var tree = Trees[row, col];
            for (int i = row - 1; i >= 0; i--)
            {
                if (Trees[i, col] >= tree)
                {
                    return false;
                }
            }
            return true;
        }
        private bool visibleFromBottom(int row, int col)
        {
            var tree = Trees[row, col];
            for (int i = row + 1; i < Trees.GetLength(0); i++)
            {
                if (Trees[i, col] >= tree)
                {
                    return false;
                }
            }
            return true;
        }
        private int distanceToLeft(int row, int col)
        {
            var tree = Trees[row, col];
            int distance = 0;
            for (int i = col - 1; i >= 0; i--)
            {
                distance++;
                if (Trees[row, i] >= tree)
                {
                    return distance;                    
                }
            }
            return distance;
        }
        private int distanceToRight(int row, int col)
        {
            var tree = Trees[row, col];
            int distance = 0;
            for (int i = col + 1; i < Trees.GetLength(0); i++)
            {
                distance++;
                if (Trees[row, i] >= tree)
                {
                    return distance;
                }
            }
            return distance;
        }
        private int distanceToTop(int row, int col)
        {
            var tree = Trees[row, col];
            int distance = 0;
            for (int i = row - 1; i >= 0; i--)
            {
                distance++;
                if (Trees[i, col] >= tree)
                {
                    return distance;
                }
            }
            return distance;
        }
        
        private int distanceToBottom(int row, int col)
        {
            var tree = Trees[row, col];
            int distance = 0;
            for (int i = row + 1; i < Trees.GetLength(0); i++)
            {
                distance++;
                if (Trees[i, col] >= tree)
                {
                    return distance;
                }
            }
            return distance;
        }
    }
}
