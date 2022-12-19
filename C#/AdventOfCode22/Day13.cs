using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Nodes;
using System.Threading.Tasks;

namespace AdventOfCode22
{
    public class Day13
    {
        public int Part1_Sum { get; set; }
        public int Part2_Sum { get; set; }
        private string Input { get; set; }
        public Day13(string input) 
        {
            Input = input;
        }
        
        public void Process()
        {
            Part1_Sum = GetPackets(Input).Chunk(2).Select((packet, sum) => Compare(packet[0], packet[1]) < 0 ? sum + 1 : 0).Sum();
            var divider = GetPackets("[[2]]\n[[6]]").ToList();
            var packets = GetPackets(Input).Concat(divider).ToList();
            packets.Sort(Compare);
            Part2_Sum = (packets.IndexOf(divider[0]) + 1) * (packets.IndexOf(divider[1]) + 1);
        }
        IEnumerable<JsonNode> GetPackets(string input) =>
        from line in input.Split("\n")
        where !string.IsNullOrEmpty(line)
        select JsonNode.Parse(line);
        int Compare(JsonNode packet_left, JsonNode packet_right)
        {
            if (packet_left is JsonValue && packet_right is JsonValue)
            {
                return (int)packet_left - (int)packet_right;
            }
            else
            {
                //Wenn alle Items Gleich sind, dann werden die Größen der Arrays verglichen
                var packet_left_array = packet_left as JsonArray ?? new JsonArray((int)packet_left);
                var packet_right_array = packet_right as JsonArray ?? new JsonArray((int)packet_right);
                return Enumerable.Zip(packet_left_array, packet_right_array)
                    .Select(p => Compare(p.First, p.Second))
                    .FirstOrDefault(c => c != 0, packet_left_array.Count - packet_right_array.Count);
            }
        }
    }
}
