using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode22
{
    public class Day6
    {
        private string Input { get; set; }
        public Day6(string input)
        {
            Input = input;
        }
        public int Result { get; set; }

        public void Process(int marker_length)
        {
            string input = Input;
            for (int i = 0; i < input.Length; i++)
            {
                var input_sub = input.Substring(i, marker_length);
                bool is_not_multiple = false;
                for (int j = 0; j < marker_length -1; j++)
                {
                    var count = input_sub.Count(c => c == input_sub[j]);
                    if (count < 2)
                    {
                        is_not_multiple = true;
                    }
                    else
                    {
                        is_not_multiple = false;
                        break;
                    }
                }
                if (is_not_multiple)
                {
                    Result = i + marker_length;
                    break;
                }
            }
        }
    }
}
