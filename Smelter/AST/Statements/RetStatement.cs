using Smelter.Interfaces;
using Smelter.Objects;

namespace Smelter.AST.Statements
{
    public class RetStatement : IStatement
    {
        public Token Token { get; set; }
        public IExpression Value { get; set; }

        public RetStatement(Token token) => Token = token;

        public IObj Evaluate(/*Memory memory*/)
        {
            var value = Value.Evaluate(/*memory*/);
            //if (value is Error)
            //    return value;

            return new Ret(value);
        }

        public override string ToString() =>
            $"{Token.Literal} {StringHelper.DefaultOrNull(Value)};";
    }
}
