using Smelter.Interfaces;
using Smelter.Objects;

namespace Smelter.AST.Statements
{
    public class ExpressionStatement : IStatement
    {
        public Token Token { get; set; }
        public IExpression Expression { get; set; }

        public ExpressionStatement(Token token) => Token = token;

        public ExpressionStatement(Token token, IExpression expression) :
            this(token) => Expression = expression;

        public IObj Evaluate(Environment environment) =>
            Expression.Evaluate(environment);

        public override string ToString() =>
            StringHelper.DefaultOrNull(Expression);
    }
}
