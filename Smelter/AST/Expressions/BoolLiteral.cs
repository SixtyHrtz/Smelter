using Smelter.Interfaces;

namespace Smelter.AST.Expressions
{
    public class BoolLiteral : IExpression
    {
        public Token Token { get; set; }
        public bool Value { get; set; }

        public BoolLiteral(Token token) => Token = token;

        //public Object Evaluate(Memory memory)
        //{
        //    return new Boolean(Value);
        //}

        public override string ToString() => Token.Literal;
    }
}
