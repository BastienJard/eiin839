using System;

namespace MethodExtern
{
    class Program
    {
        static void Main(string[] args)
        {
            if (args.Length != 0)
            {
                Console.WriteLine("Hello " + args[0] + " and " + args[1]);
            }
            else
            {
                Console.WriteLine("Hello world !!");
            }
        }
    }
}
