/*
    Problem: Rail Way
    Link: https://open.kattis.com/problems/railway
*/
using System;
using System.Collections.Generic;

namespace Toy_Railway
{
    class Program
    {
        struct Node
        {
            public int Gate;
            public int Cost;
            public int PrevGate;
            public Node(int gate, int cost, int prevGate) { Gate = gate; Cost = cost; PrevGate = prevGate; }
        }
        static void Main(string[] args)
        {
            int[] data = Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
            int stationNr = data[0], edgeNr = data[1];
            int?[] gate = new int?[stationNr * 3];
            int?[] minCosts = new int?[stationNr * 3];
            int[] trace = new int[stationNr * 3];
            for (int _ = 0; _ < edgeNr; _++)
            {
                string[] strData = Console.ReadLine().Split();
                int uS = int.Parse(strData[0].Substring(0, strData[0].Length - 1)) - 1;
                int uG = strData[0][strData[0].Length - 1] - 'A';
                int u = 3 * uS + uG;
                int vS= int.Parse(strData[1].Substring(0, strData[1].Length - 1)) - 1;
                int vG = strData[1][strData[1].Length - 1] - 'A';
                int v = 3 * vS + vG;
                gate[u] = v;
                gate[v] = u;
            }
            Queue<Node> queue = new Queue<Node>();
            if (gate[1] != null) queue.Enqueue(new Node((int)gate[1], 1, 0));
            if (gate[2] != null) queue.Enqueue(new Node((int)gate[2], 1, 0));

            while (queue.Count > 0 && minCosts[0] == null)
            {
                Node node = queue.Dequeue();
                if (!(minCosts[node.Gate] <= node.Cost))
                {
                    trace[node.Gate] = node.PrevGate;
                    minCosts[node.Gate] = node.Cost;
                    int root = node.Gate / 3 * 3;
                    if (node.Gate == root)
                    {
                        if (gate[root + 1] != null) queue.Enqueue(new Node((int)gate[root + 1], node.Cost + 1, node.Gate));
                        if (gate[root + 2] != null) queue.Enqueue(new Node((int)gate[root + 2], node.Cost + 1, node.Gate));
                    }
                    else if (gate[root] != null) queue.Enqueue(new Node((int)gate[root], node.Cost + 1, node.Gate));
                }
            }
            if (minCosts[0] == null) Console.WriteLine("Impossible");
            else
            {
                char[] result = new char[stationNr];
                for (int idx = 0; idx < stationNr; idx++) result[idx] = 'B';
                int current = 0;
                do
                {
                    if (current % 3 != 0) result[current / 3] = (char) (current % 3 + 'A');
                    int next = (int) gate[current];
                    if (next % 3 != 0) result[next / 3] = (char)(next % 3 + 'A');
                    current = trace[current];
                } while (current != 0);
                Console.WriteLine(new string(result));
            }
        }
    }
}
