using Smelter.Interfaces;

namespace Smelter.AST.Expressions
{
    public class IntLiteral : IExpression
    {
        public Token Token { get; set; }
        public int Value { get; set; }

        public IntLiteral(Token token) => Token = token;

        //public Object Evaluate(Memory memory)
        //{
        //    return new Integer(Value);
        //}

        public override string ToString() => Token.Literal;
    }
}
