/*
    Problem: Digi Comp II
    Link: https://open.kattis.com/problems/digicomp2
*/
using System;
using System.Collections.Generic;

namespace Digi_Comp_II
{
    class Program
    {
        static void Main(string[] args)
        {
            long[] data = Array.ConvertAll(Console.ReadLine().Split(), long.Parse);
            long ballNr = data[0], switchNr = data[1] + 1;
            int[,] map = new int[switchNr, 2];
            int[] hold = new int[switchNr];
            long[] counts = new long[switchNr];
            bool[] states = new bool[switchNr];
            for (int index = 1; index < switchNr; index++)
            {
                string[] strData = Console.ReadLine().Split();
                states[index] = strData[0] == "L";
                map[index, 0] = int.Parse(strData[1]);
                map[index, 1] = int.Parse(strData[2]);
                hold[map[index, 0]]++;
                hold[map[index, 1]]++;
            }
            counts[1] = ballNr;
            Queue<int> queue = new Queue<int>();
            for (int index = 1; index < switchNr; index++) if (hold[index] == 0) queue.Enqueue(index);

            while (queue.Count > 0)
            {
                int index = queue.Dequeue();
                if (index == 0) continue;
                int left = map[index, 0], right = map[index, 1];
                long half = counts[index] / 2;
                if (counts[index] % 2 == 1)
                {
                    counts[left] += states[index] ? 1 : 0;
                    counts[right] += states[index] ? 0 : 1;
                    states[index] ^= true;
                }
                counts[left] += half;
                counts[right] += half;
                hold[left]--;
                hold[right]--;
                if (hold[left] == 0) queue.Enqueue(left);
                if (hold[right] == 0 && left != right) queue.Enqueue(right);
            }
            for (int index = 1; index < switchNr; index++) Console.Write(states[index] ? 'L' : 'R');
            Console.WriteLine();
        }
    }
}