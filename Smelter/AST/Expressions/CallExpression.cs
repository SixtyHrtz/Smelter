using Smelter.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace Smelter.AST.Expressions
{
    public class CallExpression : IExpression
    {
        public Token Token { get; set; }
        public IExpression Method { get; set; }
        public List<IExpression> Arguments { get; set; }

        public CallExpression(Token token, IExpression expression, List<IExpression> arguments)
        {
            Token = token;
            Method = expression;
            Arguments = arguments;
        }

        //public Object Evaluate(Memory memory)
        //{
        //    throw new NotImplementedException();
        //}

        public override string ToString()
        {
            var arguments = string.Join(", ", Arguments.Select(x => x.ToString()));
            return $"{Method}({arguments})";
        }
    }
}
