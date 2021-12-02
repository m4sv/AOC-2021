using System.Collections.Generic;
using System.Linq;

namespace AOC_2021
{
    public class Day2
    {
        static List<Vector> GetInput()
        {
            var lines = System.IO.File.ReadAllLines("inputs/day2.txt");
            var vectors = lines.Select(x => new Vector(x)).ToList();
            return vectors;
        }

        public static string Part1()
        {
            var vectors = GetInput();
            var result = vectors.Sum(u => u.x) * vectors.Sum(v => v.y);
            return $"D2P1: Multiplied horizontal and vertical position: {result}";
        }

        public static string Part2()
        {
            var xSum = 0;
            var ySum = 0;
            var aim = 0;
            var vectors = GetInput();
            foreach (var vector in vectors)
            {
                xSum += vector.x;
                aim += vector.y;
                ySum += vector.x * aim;
            }
            var result = xSum * ySum;
            return $"D2P2: Aim adjusted multiplication: {result}";
        }

        public class Vector
        {
            public Vector() { }
            public Vector(string input)
            {
                x = 0;
                y = 0;
                var segments = input.Split(' ');
                if(segments.Length > 1)
                {
                    int.TryParse(segments[1], out int value);
                    switch (segments[0])
                    {
                        case "forward":
                            x = value;
                            break;
                        case "down":
                            y = value;
                            break;
                        case "up":
                            y = -value;
                            break;
                    }
                }
            }
            public int x { get; set; }
            public int y { get; set; }
        }
    }
}
