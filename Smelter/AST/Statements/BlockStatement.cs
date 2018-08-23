using Smelter.Interfaces;
using System.Collections.Generic;

namespace Smelter.AST.Statements
{
    public class BlockStatement : IStatement
    {
        public Token Token { get; set; }
        public List<IStatement> Statements { get; set; }

        public BlockStatement(Token token)
        {
            Token = token;
            Statements = new List<IStatement>();
        }

        //public Object Evaluate(Memory memory)
        //{
        //    foreach (Statement stmt in Statements)
        //    {
        //        Object value = stmt.Evaluate(memory);

        //        if (value is Error)
        //            return value;
        //        //if (value is Return || value is Error)
        //        //    return value;
        //    }

        //    return null;
        //}

        public override string ToString()
        {
            var result = string.Empty;
            foreach (var statement in Statements)
                result += $"{statement}\n";

            return result;
        }
    }
}
