using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode22
{
    public class Day11
    {
        public string Input { get; set; }
        public long Part1_Sum { get; set; }
        public long Part2_Sum { get; set; }
        public Day11(string input)
        {
            Input = input;            
        }
        public void Part1()
        {
            var monkeys = PlayRounds(20, true);
            var firstTwo = GetFirstTwoMonkeys(monkeys);
            Part1_Sum = firstTwo[0].inspectedItems * firstTwo[1].inspectedItems;
        }
        public void Part2()
        {
            var monkeys = PlayRounds(10_000, false);
            var firstTwo = GetFirstTwoMonkeys(monkeys);
            Part2_Sum = firstTwo[0].inspectedItems * firstTwo[1].inspectedItems;
        }
        private Monkey[] PlayRounds(int rounds, bool divideby3)
        {
            var monkeys = ProcessMonkeys(Input);
            for (int i = 0; i < rounds; i++)
            {
                foreach (var monkey in monkeys)
                {
                    while (monkey.items.Any())
                    {
                        monkey.inspectedItems++;
                        var item  = monkey.items.First();
                        item = monkey.operation(item);
                        if (divideby3)
                        {
                            item = item / 3;
                        }
                        else
                        {
                            var new_mod = monkeys.Select(m => m.modulo).Aggregate((x, y) => x * y);
                            item %= new_mod;
                        }
                        var nextMonkey = item % monkey.modulo == 0 ?
                            monkey.passToMonkey :
                            monkey.passToOtherMonkey;
                        monkey.items.RemoveAt(0);
                        monkeys[nextMonkey].items.Add(item);
                    }
                }
            }
            return monkeys;
        }
        Monkey[] GetFirstTwoMonkeys(IEnumerable<Monkey> monkeys) => monkeys.OrderByDescending(m => m.inspectedItems).Take(2).ToArray();
        Monkey[] ProcessMonkeys(string input) => input.Split("\n\n").Select(addData).ToArray();

        Monkey addData(string input)
        {
            var monkey = new Monkey();
            var lines = input.Split('\n');
            foreach (var line in lines)
            {
                if (line.StartsWith("Monkey"))
                {

                }
                else if (line.Contains("Starting items:"))
                {
                    var items = line.Substring(18).Split(", ");
                    foreach (var item in items)
                    {
                        monkey.items.Add(int.Parse(item));
                    }
                }
                else if (line.Contains("Operation"))
                {
                    if (line.Contains("*"))
                    {
                        var test = line.Substring(line.Length - 3);
                        if (test.StartsWith("o"))
                        {
                            monkey.operation = old => old * old;
                        }
                        else
                        {
                            var value = int.Parse(line.Substring(25));
                            monkey.operation = old => old * value;
                        }
                    }
                    else
                    {
                        var value = int.Parse(line.Substring(25));
                        monkey.operation = old => old + value;
                    }
                }
                else if (line.Contains("Test"))
                {
                    var value = int.Parse(line.Substring(21));
                    monkey.modulo = value;
                }
                else if (line.Contains("true"))
                {
                    var value = int.Parse(line.Substring(line.Length - 1));
                    monkey.passToMonkey = value;
                }
                else if (line.Contains("false"))
                {
                    var value = int.Parse(line.Substring(line.Length - 1));
                    monkey.passToOtherMonkey = value;
                }
            }
            return monkey;
        }
    }

    class Monkey
    {
        public List<long> items = new List<long>();
        public Func<long, long> operation { get; set; }
        public long inspectedItems { get; set; }
        public int modulo { get; set; }
        public int passToMonkey { get; set; }
        public int passToOtherMonkey { get; set; }
    }
}
