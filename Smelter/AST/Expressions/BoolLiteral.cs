using Smelter.Interfaces;
using Smelter.Objects;

namespace Smelter.AST.Expressions
{
    public class BoolLiteral : IExpression
    {
        public Token Token { get; set; }
        public bool Value { get; set; }

        public BoolLiteral(Token token, bool value)
        {
            Token = token;
            Value = value;
        }

        public IObj Evaluate(Environment environment) =>
            (Value) ? Bool.True : Bool.False;

        public override string ToString() => Token.Literal;
    }
}
