using System;

namespace ExeTest
{
    class Program
    {
        static void Main(string[] args)
        {
            if (args.Length != 0)
                Console.WriteLine(args[0] + " " + args[1]);
            else
                Console.WriteLine("ExeTest <string parameter>");
        }
    }
}
