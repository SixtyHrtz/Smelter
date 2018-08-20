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
                var parser = new Parser(lexer);
                var program = parser.Parse();

                if (parser.Errors.Count > 0)
                {
                    foreach (var error in parser.Errors)
                        Console.WriteLine($"Ошибка: {error}");
                    continue;
                }

                Console.Write(program.ToString());
            }
        }
    }
}
