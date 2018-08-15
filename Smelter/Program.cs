using Smelter.Enums;
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

                Token token;
                Console.WriteLine("{0,20}{1,10}", "[Token]", "[Literal]");
                do
                {
                    token = lexer.NextToken();
                    Console.WriteLine(token.ToString());
                }
                while (token.Type != TokenType.EndOfFile);
            }
        }
    }
}
