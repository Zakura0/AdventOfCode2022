using System;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using System.Resources;
using System.IO;
using AdventOfCode22;

Console.WriteLine("Advent of Code 2022");
var d7 = new Day7(Input.Day7);
d7.Part1();
long P2_Sum = d7.Part2();
Console.WriteLine("Day 7 Part 1: {0}", d7.P1_Sum);
Console.WriteLine("Day 7 Part 2: {0}", P2_Sum);
var d8 = new Day8(Input.Day8);
d8.Part1();
d8.Part2();
Console.WriteLine("Day 8 Part 1: {0}",d8.Part1_Sum);
Console.WriteLine("Day 8 Part 2: {0}", d8.Part2_Score);
var d9 = new Day9(Input.Day9);
d9.Part1();
d9.Part2();
Console.WriteLine("Day 9 Part 1: {0}", d9.Part1_Sum);
Console.WriteLine("Day 9 Part 2: {0}", d9.Part2_Sum);
var d10 = new Day10(Input.Day10);
d10.Part1();
Console.WriteLine("Day 10 Part 1: {0}", d10.Part1_Sum);
d10.Part2();
Console.WriteLine();
var d11 = new Day11(Input.Day11);
d11.Part1();
Console.WriteLine("Day 11 Part 1: {0}", d11.Part1_Sum);
d11.Part2();
Console.WriteLine("Day 11 Part 2: {0}", d11.Part2_Sum);
Console.Read();




