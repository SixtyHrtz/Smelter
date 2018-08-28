using Smelter.AST;
using System.Collections.Generic;

namespace Smelter
{
    public class Script
    {
        private string text;
        private Lexer lexer;
        private Parser parser;
        private SmeltProgram program;

        public List<string> ParserErrors => parser.Errors;

        //public List<string> EvaluateErrors
        //{
        //    get { return program.Errors; }
        //}

        public bool Compile(string text)
        {
            this.text = text;
            lexer = new Lexer(text);
            parser = new Parser(lexer);
            program = parser.Parse();

            return parser.Errors.Count == 0;
        }

        public string Run()
        {
            var environment = new Environment();
            return string.Join("\n", program.Evaluate(environment)) + "\n";
        }

        public override string ToString() => program.ToString();
    }
}
