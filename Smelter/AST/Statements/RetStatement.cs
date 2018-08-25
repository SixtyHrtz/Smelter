using Smelter.Interfaces;
using Smelter.Objects;

namespace Smelter.AST.Statements
{
    public class RetStatement : IStatement
    {
        public Token Token { get; set; }
        public IExpression Value { get; set; }

        public RetStatement(Token token) => Token = token;

        public IObject Evaluate(/*Memory memory*/)
        {
            return null;

            //Object value = Value.Evaluate(memory);
            //if (value is Error)
            //    return value;

            //return new Return(value);
        }

        public override string ToString() =>
            $"{Token.Literal} {StringHelper.DefaultOrNull(Value)};";
    }
}
