using Smelter.AST.Expressions;
using Smelter.Interfaces;
using Smelter.Objects;

namespace Smelter.AST.Statements
{
    public class DefStatement : IStatement
    {
        public Token Token { get; set; }
        public Identifier Name { get; set; }
        public IExpression Value { get; set; }

        public DefStatement(Token token) => Token = token;

        public IObject Evaluate()
        {
            return null;
        }

        public override string ToString() =>
            $"{Token.Literal} {Name} = {StringHelper.DefaultOrNull(Value)};";
    }
}
