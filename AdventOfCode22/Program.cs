﻿using System;
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
Console.Read();



