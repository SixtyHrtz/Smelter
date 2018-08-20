using Smelter.Interfaces;

namespace Smelter.AST.Expressions
{
    public class Identifier : IExpression
    {
        public Token Token { get; set; }
        public string Value { get; set; }

        public Identifier(Token token)
        {
            Token = token;
            Value = token.Literal;
        }

        public override string ToString() => Value;
    }
}
