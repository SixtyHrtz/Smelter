using Smelter.Interfaces;

namespace Smelter.AST.Statements
{
    public class ExpressionStatement : IStatement
    {
        public Token Token { get; set; }
        public IExpression Expression { get; set; }

        public ExpressionStatement(Token token) => Token = token;

        public ExpressionStatement(Token token, IExpression expression) :
            this(token) => Expression = expression;

        //public Object Evaluate(Memory memory)
        //{
        //    return Expression.Evaluate(memory);
        //}

        public override string ToString() => StringHelper.DefaultOrNull(Expression);
    }
}
