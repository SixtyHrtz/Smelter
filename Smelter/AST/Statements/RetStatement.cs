using Smelter.Interfaces;
using Smelter.Objects;

namespace Smelter.AST.Statements
{
    public class RetStatement : IStatement
    {
        public Token Token { get; set; }
        public IExpression Value { get; set; }

        public RetStatement(Token token) => Token = token;

        public IObj Evaluate(Environment environment)
        {
            var value = Value.Evaluate(environment);
            if (value is Err)
                return value;

            return new Ret(value);
        }

        public override string ToString() =>
            $"{Token.Literal} {StringHelper.DefaultOrNull(Value)};";
    }
}
