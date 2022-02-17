/*
    Problem: Coast Length
    Link: https://open.kattis.com/problems/coast
*/
using System;
using System.Collections.Generic;

namespace Coast_Length
{
    struct Point
    {
        public int X { get; set; }
        public int Y { get; set; }
        public Point(int x, int y)
        {
            X = x;
            Y = y;
        }
        public static Point operator+(Point a, Point b)
        {
            return new Point(a.X + b.X, a.Y + b.Y);
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            int[] data = Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
            int rowNr = data[0], colNr = data[1];
            bool?[,] map = new bool?[rowNr + 2, colNr + 2];
            bool[,] passed = new bool[rowNr + 2, colNr + 2];
            for (int row = 1; row <= rowNr; row++)
            {
                string line = Console.ReadLine();
                for (int col = 1; col <= colNr; col++) if (line[col - 1] == '1') map[row, col] = true;
            }
            Queue<Point> queue = new Queue<Point>();
            Point[] dirs = new Point[] { new Point(0, -1), new Point(-1, 0), new Point(1, 0), new Point(0, 1) };
            queue.Enqueue(new Point(0, 0));
            passed[0, 0] = true;
            while (queue.Count > 0)
            {
                Point cur = queue.Dequeue();
                map[cur.X, cur.Y] = false;
                foreach (Point dir in dirs) {
                    Point nxt = cur + dir;
                    if (
                        nxt.X >= 0
                        && nxt.X < rowNr + 2
                        && nxt.Y >= 0
                        && nxt.Y < colNr + 2
                        && passed[nxt.X, nxt.Y] == false
                        && map[nxt.X, nxt.Y] == null)
                    {
                        queue.Enqueue(new Point(nxt.X, nxt.Y));
                        passed[nxt.X, nxt.Y] = true;
                    }
                }
            }
            int res = 0;
            for (Point cur = new Point(1, 1); cur.X <= rowNr; cur.X++)
            {
                for (cur.Y = 1; cur.Y <= colNr; cur.Y++)
                {
                    if (map[cur.X, cur.Y] == true) foreach (Point dir in dirs)
                    {
                        Point nxt = cur + dir;
                        if (map[nxt.X, nxt.Y] == false) res += 1;
                    }
                }
            }
            Console.WriteLine(res);
        }
    }
}

