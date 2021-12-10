using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace AOC_2021
{
    public class Day10
    {
        static List<string> GetInput()
        {
            var result = System.IO.File.ReadAllLines("inputs/day10.txt").ToList();
            return result;
        }

        public static string Part1()
        {
            var input = GetInput();
            var score = 0;
            foreach(var line in input)
            {
                score += GetLineScore(line);
            }
            return $"D10P1: Score {score}";
        }

        public static string Part2()
        {
            var input = GetInput();

            var incompleteLines = input.Where(x => GetLineScore(x) == 0).ToList();
            var incompleteStackScores = incompleteLines.Select(x => ScoreIncompleteStack(GetIncompleteStack(x))).ToList();
            incompleteStackScores.Sort();

            var middleScore = incompleteStackScores[incompleteStackScores.Count() / 2];
            return $"D10P2: Middle score {middleScore}";
        }

        static int GetLineScore(string line)
        {
            var stack = new Stack<char>();
            for(int i=0; i< line.Length; i++)
            {
                var c = line[i];
                if (IsOpener(c))
                {
                    stack.Push(c);
                    continue;
                }
                if(IsCloser(c))
                {
                    if (stack.Count == 0) return ScoreCorruption(c);

                    var topStack = stack.Pop();
                    if (!IsPair(topStack, c))
                    {
                        return ScoreCorruption(c);
                    }
                }
            }
            return 0;
        }

        static int ScoreCorruption(char character)
        {
            if (character == ')') return 3;
            if (character == ']') return 57;
            if (character == '}') return 1197;
            if (character == '>') return 25137;
            return 0;
        }

        static bool IsOpener(char character)
        {
            return character == '(' || 
                   character == '[' || 
                   character == '{' || 
                   character == '<';
        }
        static bool IsCloser(char character)
        {
            return character == ')' || 
                   character == ']' || 
                   character == '}' || 
                   character == '>';
        }

        static bool IsPair(char c1, char c2)
        {
            if      (c1 == '(' && c2 == ')') return true;
            else if (c1 == '[' && c2 == ']') return true;
            else if (c1 == '{' && c2 == '}') return true;
            else if (c1 == '<' && c2 == '>') return true;
            return false;
        }

        static double ScoreIncompleteStack(Stack<char> stack)
        {
            double score = 0;
            while(stack.Count > 0)
            {
                score = (5 * score) + ScoreCloser(stack.Pop());
            }
            return score;
        }

        static int ScoreCloser(char opener)
        {
            if (opener == '(') return 1;
            if (opener == '[') return 2;
            if (opener == '{') return 3;
            if (opener == '<') return 4;
            return 0;
        }
        static Stack<char> GetIncompleteStack(string line)
        {
            var stack = new Stack<char>();
            for (int i = 0; i < line.Length; i++)
            {
                var c = line[i];
                if (IsOpener(c))
                {
                    stack.Push(c);
                }
                else if (IsCloser(c) && stack.Count > 0)
                {
                    stack.Pop();
                }
            }
            return stack;
        }

    }
}