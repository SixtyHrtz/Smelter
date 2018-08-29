using Smelter.Interfaces;
using Smelter.Objects;

namespace Smelter.AST.Expressions
{
    public class StrLiteral : IExpression
    {
        public Token Token { get; set; }
        public string Value { get; set; }

        public StrLiteral(Token token)
        {
            Token = token;
            Value = token.Literal;
        }

        public IObj Evaluate(Environment environment) => new Str(Value);

        public override string ToString() => Token.Literal;
    }
}
