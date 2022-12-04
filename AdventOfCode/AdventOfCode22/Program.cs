using System;
using System.Diagnostics.CodeAnalysis;

string input = System.IO.File.ReadAllText(@"C:\Users\Simon Kazemi\Downloads\input.txt");
var lines = input.Split('\n');
Array.Resize(ref lines, lines.Length - 1);
int totalScore = 0;
foreach (var line in lines)
{
    var game = line.Replace(" ", "");
    switch (game)
    {
        case "AX":
            totalScore += 3;
            break;
        case "AY":
            totalScore += 4;
            break;
        case "AZ":
            totalScore += 8;
            break;
        case "BX":
            totalScore += 1;
            break;
        case "BY":
            totalScore += 5;
            break;
        case "BZ":
            totalScore += 9;
            break;
        case "CX":
            totalScore += 2;
            break;
        case "CY":
            totalScore += 6;
            break;
        case "CZ":
            totalScore += 7;
            break;
    }
}
Console.WriteLine(totalScore);
Console.ReadKey();
