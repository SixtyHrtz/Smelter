using Smelter.Interfaces;
using Smelter.Objects;
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

        public IObj Evaluate(Environment environment)
        {
            foreach (var statement in Statements)
            {
                var value = statement.Evaluate(environment);
                if (value is Ret || value is Err)
                    return value;
            }

            return Null.Ref;
        }

        public override string ToString()
        {
            var result = string.Empty;
            foreach (var statement in Statements)
                result += $"{statement}\n";

            return result;
        }
    }
}
