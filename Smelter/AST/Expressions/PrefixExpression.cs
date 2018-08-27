using Smelter.Interfaces;
using Smelter.Objects;

namespace Smelter.AST.Expressions
{
    public class PrefixExpression : IExpression
    {
        public Token Token { get; set; }
        public string Operator { get; set; }
        public IExpression Right { get; set; }

        public PrefixExpression(Token token)
        {
            Token = token;
            Operator = token.Literal;
        }

        public IObj Evaluate(/*Memory memory*/)
        {
            var right = Right.Evaluate(/*memory*/);
            if (right is Err)
                return right;

            switch (Operator)
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
                    return new Err($"Оператор не поддерживается: {Operator}{right.GetType()}");
            }

            return new Err("Тип данных не поддерживает префиксное " +
                $"выражение: {Operator}{right.GetType()}");
        }

        public override string ToString() =>
            $"({Operator}{Right.ToString()})";
    }
}
