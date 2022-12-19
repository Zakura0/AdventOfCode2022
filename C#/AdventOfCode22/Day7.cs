using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode22
{
    public class Day7
    {
        public string Input { get; }
        public Dictionary<string, Directory> Directories { get; set; } = new();
        public Day7(string input) => this.Input = input;
        public void Part1()
        {
            var baseDirectory = new Directory("/", null!);
            var currentDirectory = baseDirectory;
            var lines = Input.Split('\n');
            Array.Resize(ref lines, lines.Length - 1);
            Directories.Add("/", currentDirectory);
            
            foreach (var line in lines)
            {

                if (line.StartsWith("$"))
                {
                    if (line.Equals("$ cd .."))
                    {
                        currentDirectory = currentDirectory.ParentDirectory ?? baseDirectory;
                    }
                    else if (line.Equals("$ cd /"))
                    {
                        currentDirectory = baseDirectory;
                    }
                    else if (line.StartsWith("$ cd"))
                    {
                        var directoryName = line.Split(' ')[2];
                        currentDirectory = Directories[currentDirectory.Name + "/" + directoryName];
                    }
                }
                else if (line.StartsWith("dir"))
                {
                    var directoryName = line.Split(' ')[1];
                    Directories.TryAdd(currentDirectory.Name + "/" + directoryName, new Directory(currentDirectory.Name + "/" + directoryName, currentDirectory));
                }
                else
                {
                    var fileSize = long.Parse(line.Split(' ')[0]);
                    currentDirectory.IncreaseSize(fileSize);
                }
            }
        }
        public long P1_Sum => Directories.Values.Where(d => d.Size <= 100000).Sum(d => d.Size);

        public long Part2()
        {
            var requiredSpace = 30000000;
            var freeSpace = (70000000 - Directories["/"].Size);
            var delete = Directories.Values.OrderBy(d => d.Size).First(d => d.Size >= requiredSpace - freeSpace);
            return delete.Size;
        }

    }
    public class Directory
    {
        public long Size { get; set; }
        public Directory ParentDirectory { get; set; }
        public string Name { get; set; }
        public Directory(string name, Directory parentDirectory)
        {
            this.Name = name;
            this.ParentDirectory = parentDirectory;
        }
        internal void IncreaseSize(long size)
        {
            this.Size += size;
            if (ParentDirectory != null)
            {
                ParentDirectory.IncreaseSize(size);
            }
        }

    }
}
