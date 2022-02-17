/*
    Problem: No Duplicates
    Link: https://open.kattis.com/problems/nodup
*/
using System;
using System.Collections.Generic;

namespace nodup
{
    internal class Program
    {
        static void Main()
        {
            string[] words = Console.ReadLine().Split();
            Console.WriteLine(words.Length == new HashSet<string>(words).Count ? "yes" : "no");
        }
    }
}