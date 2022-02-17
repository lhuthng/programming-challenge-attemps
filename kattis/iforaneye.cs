/*
    Problem: An I for an Eye
    Link: https://open.kattis.com/problems/iforaneye
*/
using System;
using System.Collections.Generic;
using System.Text;

namespace Kattis
{
    internal class Substitute
    {
        private readonly string sequence;
        private readonly char character;
        private int remains;
        private bool uppercase = false;
        public int Remains { get { return remains; } }
        public char Character { get { return uppercase ? char.ToUpper(character) : character; } }
        public int Index { get { return sequence.Length - remains; } }
        public bool IsFinished { get { return remains == 0; } }
        public Substitute(string sequence, char character)
        {
            this.sequence = sequence; 
            this.character = character;
            this.remains = sequence.Length;
        }
        private Substitute Clone()
        {
            Substitute clone = new Substitute(sequence, character);
            clone.remains = remains;
            clone.uppercase = uppercase;
            return clone;
        }
        public bool IsMatched(char letter)
        {
            if (Index != 0) return letter == sequence[Index]; 
            else
            {
                uppercase = letter == char.ToUpper(letter);
                return char.ToLower(letter) == sequence[Index];
            }
        }
        public Substitute GetNext()
        {
            Substitute result = this;
            if (Index == 0)
            {
                result = Clone();
                result.remains--;
            }
            else remains--; 
            return result;
        }
    }
    internal class Program
    {
        private static List<Substitute> defaultSubstitutes;
        static void Main()
        {
            defaultSubstitutes = new List<Substitute>()
            {
                new Substitute("at", '@'),
                new Substitute("and", '&'),
                new Substitute("one", '1'),
                new Substitute("won", '1'),
                new Substitute("to", '2'),
                new Substitute("too", '2'),
                new Substitute("two", '2'),
                new Substitute("for", '4'),
                new Substitute("four", '4'),
                new Substitute("bea", 'b'),
                new Substitute("be", 'b'),
                new Substitute("bee", 'b'),
                new Substitute("sea", 'c'),
                new Substitute("see", 'c'),
                new Substitute("eye", 'i'),
                new Substitute("oh", 'o'),
                new Substitute("owe", 'o'),
                new Substitute("are", 'r'),
                new Substitute("you", 'u'),
                new Substitute("why", 'y'),
            };
            for (int _ = 0, __ = int.Parse(Console.ReadLine()); _ < __; _++)
            {
                string sequence = Console.ReadLine();
                StringBuilder builder = new StringBuilder();
                int left = -1, right = -1;
                Queue<Substitute> queue = new Queue<Substitute>();
                for (int index = 0; index < sequence.Length; index++)
                {
                    builder.Append(sequence[index]);
                    for (int length = queue.Count; length > 0; length--)
                        TryAppend(queue, queue.Dequeue(), sequence, index, ref left, ref right, builder);
                    foreach (Substitute substitute in defaultSubstitutes)
                        TryAppend(queue, substitute, sequence, index, ref left, ref right, builder);
                }
                Console.WriteLine(builder.ToString());
            }
        }
        private static void TryAppend(
            Queue<Substitute> queue, 
            Substitute substitute, 
            string sequence, 
            int index, 
            ref int left, ref int right, 
            StringBuilder builder)
        {
            if (
                substitute.Remains <= sequence.Length - index
                && (left >= index - substitute.Index || right < index - substitute.Index)
                && substitute.IsMatched(sequence[index]))
            {
                Substitute newSubstitute = substitute.GetNext();
                if (newSubstitute.IsFinished)
                {                    
                    if (left >= index - (substitute.Index - 1)) builder.Remove(builder.Length - (index - left), index - left);
                    else builder.Remove(builder.Length - (substitute.Index), substitute.Index); 
                    left = index - substitute.Index + 1;
                    right = index;
                    builder.Append(substitute.Character);
                }
                else queue.Enqueue(newSubstitute);
            }
        }
    }
}

