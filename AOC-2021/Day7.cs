using System;
using System.Collections.Generic;
using System.Linq;

namespace AOC_2021
{
    public class Day7
    {
        static List<int> GetInput()
        {
            var lines = System.IO.File.ReadAllLines("inputs/day7.txt").ToList();
            var result = lines[0].Split(',').Select(x => int.Parse(x)).ToList();
            return result;
        }

        public static string Part1()
        {
            var lowest = int.MaxValue;
            var input = GetInput();
            foreach(var crab in input)
            {
                var cost = CalculateCost(crab, input);

                if (cost < lowest) lowest = cost;
            }
            return $"D7P1: Lowest Fuel consumption: {lowest}";
        }

        static int CalculateCost(int position, List<int> crabList)
        {
            var cost = 0;
            foreach (var crab in crabList)
            {
                if (crab > position)      cost += crab - position;
                else if (crab < position) cost += position - crab;
            }
            return cost;
        }

        public static string Part2()
        {
            var input = GetInput();
            var floor = Math.Floor(input.Average());
            var ciel = Math.Ceiling(input.Average());
            var floorCost = CalculateCrabCost(floor, input);
            var cielCost = CalculateCrabCost(ciel, input);

            var lowest = floorCost > cielCost ? cielCost : floorCost;

            return $"D7P2: Lowest Fuel consumption {lowest}";
        }

        static double CalculateCrabCost(double position, List<int> crabList)
        {
            double cost = 0;
            foreach (var crab in crabList)
            {
                double diff = 0;
                if (crab > position)       diff = crab - position;
                else if (crab < position)  diff = position - crab;
                
                for (var i = 1; i <= diff; i++) cost += i;
            }
            return cost;
        }
    }

}
