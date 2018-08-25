using Smelter.Interfaces;
using Smelter.Objects;

namespace Smelter.AST.Expressions
{
    public class IntLiteral : IExpression
    {
        public Token Token { get; set; }
        public int Value { get; set; }

        public IntLiteral(Token token) => Token = token;

        public IObject Evaluate(/*Memory memory*/) => new Int(Value);

        public override string ToString() => Token.Literal;
    }
}
