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

        public IObj Evaluate(Environment environment)
        {
            var value = Value.Evaluate(environment);
            if (value is Err)
                return value;

            environment.RAM[Name.Value] = value;
            return Null.Ref;
        }

        public override string ToString() =>
            $"{Token.Literal} {Name} = {StringHelper.DefaultOrNull(Value)};";
    }
}
