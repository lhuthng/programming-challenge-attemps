/*
    Problem: Wood Cutting
    Link: https://open.kattis.com/problems/woodcutting
*/
using System;

namespace Wood_Cutting
{
    class Program
    {
        static void Main(string[] args)
        {
            for (int _ = 0, __ = int.Parse(Console.ReadLine()); _ < __; _++)
            {
                int n = int.Parse(Console.ReadLine());
                int[] arr = new int[n];
                for (int idx = 0; idx < n; idx++)
                {
                    int[] data = Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
                    for (int idx2 = 1; idx2 <= data[0]; idx2++) 
                        arr[idx] += data[idx2];
                }
                Array.Sort(arr);
                double sum = arr[0];
                for (int idx = 1; idx < n; idx++) sum += arr[idx] += arr[idx - 1];
                Console.WriteLine(sum / n);
            }
        }
    }
}

