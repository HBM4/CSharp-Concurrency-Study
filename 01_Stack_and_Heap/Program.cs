using System.Diagnostics;

namespace _01_Stack_and_Heap
{
    internal class Program
    {
        struct Points
        {
        public int X { get; set; }
        public int Y { get; set; }
        public Points(int x = 0, int y = 0)
        {
            X = x;
            Y = y;
        }
        public override string ToString()
        {
            return $"({X},{Y})";
        }
    }

    class Pointc
    {
        public int X { get; set; }
        public int Y { get; set; }
        public Pointc(int x = 0, int y = 0)
        {
            X = x;
            Y = y;
        }
        public override string ToString()
        {
            return $"({X},{Y})";
        }
    }

    static void Main()
        {
            int n1 = 10;
            int n2 = n1;

            Points ps1 = new Points(1, 2);
            Points ps2 = ps1;

            Pointc pc1 = new Pointc(1, 2);
            Pointc pc2 = pc1;

            n2 = 50;
            ps2.X = 5;
            pc2.X = 5;

            Console.WriteLine($"{n1} : {n2}");
            Console.WriteLine($"{ps1} : {ps2}");
            Console.WriteLine($"{pc1} : {pc2}");
        }
        //10 : 50
        //(1,2) : (5,2)
        //(5,2) : (5,2)
    }
}