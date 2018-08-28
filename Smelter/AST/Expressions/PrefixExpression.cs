using Smelter.Interfaces;
using Smelter.Objects;

namespace Smelter.AST.Expressions
{
    public class PrefixExpression : IExpression
    {
        public Token Token { get; set; }
        public IExpression Right { get; set; }

        public PrefixExpression(Token token) => Token = token;

        public IObj Evaluate(/*Memory memory*/)
        {
            var right = Right.Evaluate(/*memory*/);
            if (right is Err)
                return right;

            switch (Token.Literal)
            {
                case "!":
                    if (right is Bool)
                        return !(right as Bool);
                    break;
                case "-":
                    if (right is Int)
                        return -(right as Int);
                    break;
                default:
                    return new Err($"Данный префиксный оператор не поддерживается: {Token.Literal}{right.Type}");
            }

            return new Err($"Для этого типа данных данный оператор недоступен: {Token.Literal}{right.Type}");
        }

        public override string ToString() =>
            $"({Token.Literal}{Right.ToString()})";
    }
}
