using System;
using System.Collections.Generic;
using System.Linq;

namespace AOC_2021
{
    public class Day6
    {
        static List<int> GetInput()
        {
            var lines = System.IO.File.ReadAllLines("inputs/day6.txt").ToList();
            var result = lines[0].Split(',').Select(x => int.Parse(x)).ToList();
            return result;
        }
        public static string Part1()
        {
            var input = GetInput();

            for (var day = 0; day < 80; day++)
            {
                var newElements = new List<int>();
                for (int i = 0; i < input.Count; i++)
                {
                    if (input[i] == 0)
                    {
                        input[i] = 6;
                        newElements.Add(8);
                    }
                    else
                    {
                        input[i] -= 1;
                    }
                }
                input.AddRange(newElements);
            }

            var result = input.Count;


            return $"D6P1: The number of fish is {result}";
        }

        public static string Part2()
        {
            var input = GetInput();
            var fishZone = new FishZone(input);
            
            for (var day = 0; day < 256; day++)
            {
                fishZone.RunFishCycle();
            }
            
            var result = fishZone.GetFishCount();

            return $"D6P2: The number of fish is {result}";
        }

        public class FishZone
        {
            public Dictionary<int, long> FishDictionary = new Dictionary<int, long>();
            public FishZone(List<int> initialFish)
            {
                var fishGroup = initialFish.GroupBy(x => x);
                foreach (var fishlevel in fishGroup)
                {
                    FishDictionary.Add(fishlevel.Key, fishlevel.Count());
                }
                FishDictionary.Add(6, 0);
                FishDictionary.Add(7, 0);
                FishDictionary.Add(8, 0);
                FishDictionary.Add(0, 0);
            }
            public void RunFishCycle()
            {
                long fishToAdd = 0;
                for(int i = 0; i < FishDictionary.Count; i++)
                {
                    var fishCount = FishDictionary[i];
                    if (i == 0)
                    {
                        fishToAdd = fishCount;
                    }
                    if (i == 8)
                    {
                        FishDictionary[i] = 0;
                    }
                    else
                    {
                        FishDictionary[i] = FishDictionary[i + 1];
                    }
                }
                FishDictionary[8] += fishToAdd;
                FishDictionary[6] += fishToAdd;
            }

            public long GetFishCount()
            {
                return FishDictionary.Sum(x => x.Value);
            }
        }

    }

}
