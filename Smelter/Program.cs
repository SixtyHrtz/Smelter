using System;

namespace Smelter
{
    class Program
    {
        static void Main(string[] args)
        {
            while (true)
            {
                Console.Write(">> ");
                var lexer = new Lexer(Console.ReadLine());
                var program = new Parser(lexer).Parse();

                foreach (var statement in program.Statements)
                    Console.WriteLine(statement.ToString());
            }
        }
    }
}
