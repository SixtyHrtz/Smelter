using System;

namespace Smelter
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Добро пожаловать в скриптовой язык Smelt!");
            Console.WriteLine("Можете вводить свой код:");

            while (true)
            {
                Console.Write(">> ");
                var script = new Script();

                if (script.Compile(Console.ReadLine()))
                    Console.Write(script.Run());
                else
                {
                    Console.WriteLine("Во время компиляции возникли ошибки!");
                    foreach (var error in script.ParserErrors)
                        Console.WriteLine($"\tОшибка: {error}");
                    continue;
                }
            }
        }
    }
}
