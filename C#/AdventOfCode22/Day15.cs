using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Runtime.ExceptionServices;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode22
{
    //               1    1    2    2
    //     0    5    0    5    0    5
    // 0 ....S.......................
    // 1 ......................S.....
    // 2 ...............S............
    // 3 ................SB..........
    // 4 ............................
    // 5 ............................
    // 6 ............................
    // 7 ..........S.......S.........
    // 8 ............................
    // 9 ............................
    //10 ....B.......................
    //11 ..S.........................
    //12 ............................
    //13 ............................
    //14 ..............S.......S.....
    //15 B...........................
    //16 ...........SB...............
    //17 ................S..........B
    //18 ....S.......................
    //19 ............................
    //20 ............S......S........
    //21 ............................
    //22 .......................B....
    public class Day15
    {
        public long Part1_Sum { get; set; }
        public long Part2_Sum { get; set; }
        Dictionary<Point, Point> sensorReports { get; set; }
        string Input { get; set; }
        public Day15(string input)
        {
            Input = input;

        }
        public void Process()
        {
            sensorReports = ParseInput();
            //Part1_Sum = CountInvalidPositionsInRow(2_000_000);
            Part1_Sum = GetIntervals(2_000_000);
            Part2_Sum = Part2(4_000_000);
        }

        long Part2(int area)
        {
            List<Tuple<long, long>> intervals = new List<Tuple<long, long>>();

            for (int row = 0; row <= area; row++)
            {
                foreach (var (sensor, beacon) in sensorReports)
                {
                    long distance = Math.Abs(sensor.X - beacon.X) + Math.Abs(sensor.Y - beacon.Y);
                    long offset = distance - Math.Abs(sensor.Y - row);
                    if (offset >= 0)
                    {
                        var lower = sensor.X - offset;
                        var upper = sensor.X + offset;
                        intervals.Add(new Tuple<long, long>(lower, upper));
                    }
                }
                intervals.Sort();
                //if (intervals[0].Item1 != 0)
                //{
                //    return row;
                //}
                long reachable = intervals[0].Item2;
                for (int i = 0; i < intervals.Count - 1; i++) 
                {
                    if (reachable >= intervals[i+1].Item1)
                    {
                        reachable = Math.Max(intervals[i + 1].Item2, reachable);
                    }
                }
                if (reachable != area)
                {
                    return (reachable + 1) * area + row;
                }
            }
            return 0;
        }

        //long Part2(int area)
        //{
        //    long result = 0;
        //    double percent = 0;
        //    for (int row = 0; row <= area; row++)
        //    {
        //        List<Tuple<int, int>> t_intervals = new List<Tuple<int, int>>();
        //        if (row % 100000 == 0)
        //        {
        //            Console.WriteLine("At " + percent + " percent.");
        //            percent += 2.5;
        //        }
        //        foreach (var (sensor, beacon) in sensorReports)
        //        {
        //            var distance = Math.Abs(sensor.X - beacon.X) + Math.Abs(sensor.Y - beacon.Y);
        //            var offset = distance - Math.Abs(sensor.Y - row);
        //            if (offset < 0)
        //            {
        //                continue;
        //            }
        //            var low_x = sensor.X - offset;
        //            var high_x = sensor.X + offset;
        //            t_intervals.Add(new Tuple<int, int>(low_x, high_x));
        //        }
        //        t_intervals.Sort();
        //        List<Pair<int, int>> intervals = new List<Pair<int, int>>();
        //        foreach (var tuple in t_intervals)
        //        {
        //            intervals.Add(new Pair<int, int>(tuple.Item1, tuple.Item2));
        //        }
        //        List<Pair<int, int>> p_intervals = new List<Pair<int, int>>();
        //        foreach (var tuple in intervals)
        //        {
        //            if (p_intervals.Count == 0)
        //            {
        //                p_intervals.Add(tuple);
        //                continue;
        //            }
        //            int super_low = p_intervals.Last().First;
        //            int super_high = p_intervals.Last().Second;
        //            if (tuple.First > super_high + 1)
        //            {
        //                p_intervals.Add(new Pair<int, int>(tuple.First, tuple.Second));
        //                continue;
        //            }
        //            p_intervals.Last().Second = Math.Max(super_high, tuple.Second);
        //        }
        //        int x = 0;
        //        foreach (var intervall in p_intervals)
        //        {
        //            if (x < intervall.First)
        //            {
        //                result = (long)(x * 4000000 + row);
        //            }
        //            x = Math.Max(x, intervall.Second + 1);
        //            if (x > area)
        //            {
        //                break;
        //            }
        //        }
        //    }
        //    return result;
        //}
        //much faster
        long GetIntervals(int row)
        {
            HashSet<int> known = new HashSet<int>();
            HashSet<int> invalid = new HashSet<int>();
            foreach (var (sensor, beacon) in sensorReports)
            {
                List<Pair<int, int>> intervals = new List<Pair<int, int>>();
                var distance = Math.Abs(sensor.X - beacon.X) + Math.Abs(sensor.Y - beacon.Y);
                var offset = distance - Math.Abs(sensor.Y - row);
                if (offset < 0)
                {
                    continue;
                }
                var low_x = sensor.X - offset;
                var high_x = sensor.X + offset;
                intervals.Add(new Pair<int, int>(low_x, high_x));
                if (beacon.Y == row)
                {
                    known.Add(beacon.Y);
                }
                intervals.Sort();
                List<Pair<int, int>> p_intervals = new List<Pair<int, int>>();
                foreach (var tuple in intervals)
                {
                    if (p_intervals.Count == 0)
                    {
                        p_intervals.Add(tuple);
                        continue;
                    }
                    int super_low = intervals.Last().First;
                    int super_high = intervals.Last().Second;
                    if (tuple.First > super_high + 1)
                    {
                        p_intervals.Add(new Pair<int, int>(tuple.First, tuple.Second));
                        continue;
                    }
                    p_intervals.Last().Second = Math.Max(super_high, tuple.Second);
                }
                foreach (var pair in p_intervals)
                {
                    for (int i = pair.First; i <= pair.Second; i++)
                    {
                        invalid.Add(i);
                    }
                }
            }
            return (invalid.Count - known.Count);
        }
        //slow af
        long CountInvalidPositionsInRow(int row)
        {
            var signals = new HashSet<Point>();
            foreach (var (sensor, beacon) in sensorReports)
            {
                var distance_x = Math.Abs(sensor.X - beacon.X);
                var distance_y = Math.Abs(sensor.Y - beacon.Y);
                var radius = distance_x + distance_y;
                if (row > sensor.Y + radius && row < sensor.Y - radius)
                {
                    continue;
                }
                var distance_row = row > sensor.Y ? (sensor.Y + radius) - row : row - (sensor.Y - radius);
                for (var i = sensor.X - distance_row; i < sensor.X + distance_row; i++)
                {
                    signals.Add(new Point(i, row));
                }
            }
            return signals.Count;
        }
        private Dictionary<Point, Point> ParseInput()
        {
            Dictionary<Point, Point> report = new Dictionary<Point, Point>();
            var lines = Input.Split('\n');
            Array.Resize(ref lines, lines.Length - 1);
            foreach (var line in lines)
            {
                var split = line.Split(':');
                var sensor_line = split[0].Split('=');
                var beacon_line = split[1].Split('=');
                var sensor_x = int.Parse(sensor_line[1].Split(',')[0]);
                var sensor_y = int.Parse(sensor_line[2]);
                var beacon_x = int.Parse(beacon_line[1].Split(',')[0]);
                var beacon_y = int.Parse(beacon_line[2]);
                report.Add(new Point(sensor_x, sensor_y), new Point(beacon_x, beacon_y));
            }
            return report;
        }
    }
    public class Pair<T1, T2>
    {
        public Pair(T1 first, T2 second)
        {
            First = first;
            Second = second;
        }

        public T1 First { get; set; }
        public T2 Second { get; set; }
    }

}
