using Smelter.AST.Statements;
using Smelter.Interfaces;
using Smelter.Objects;
using System.Collections.Generic;
using System.Linq;

namespace Smelter.AST.Expressions
{
    public class MetLiteral : IExpression
    {
        public Token Token { get; set; }
        public List<Identifier> Parameters { get; set; }
        public BlockStatement Body { get; set; }

        public MetLiteral(Token token) => Token = token;

        public IObj Evaluate(Environment environment) => Null.Ref;

        public override string ToString()
        {
            var parameters = string.Join(", ", Parameters.Select(x => x.ToString()));
            return $"{Token.Literal}({parameters}) {Body}";
        }
    }
}
