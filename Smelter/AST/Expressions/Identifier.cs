using Smelter.Interfaces;
using Smelter.Objects;

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

        public IObj Evaluate(Environment environment)
        {
            if (!environment.RAM.ContainsKey(Value))
                return new Err($"Переменная не определена: {Value}");
            return environment.RAM[Value];
        }

        public override string ToString() => Value;
    }
}
