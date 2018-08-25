using Smelter.Interfaces;
using Smelter.Objects;

namespace Smelter.AST.Expressions
{
    public class BoolLiteral : IExpression
    {
        public Token Token { get; set; }
        public bool Value { get; set; }

        public BoolLiteral(Token token) => Token = token;

        public IObject Evaluate(/*Memory memory*/) =>
            (Value) ? Bool.True : Bool.False;

        public override string ToString() => Token.Literal;
    }
}
