using Smelter.Enums;
using Smelter.Interfaces;
using Smelter.Objects;

namespace Smelter.AST.Expressions
{
    public class InfixExpression : IExpression
    {
        public Token Token { get; set; }
        public IExpression Left { get; set; }
        public IExpression Right { get; set; }

        public InfixExpression(Token token, IExpression left)
        {
            Token = token;
            Left = left;
        }

        public IObj Evaluate(Environment environment)
        {
            var left = Left.Evaluate(environment);
            if (left is Err)
                return left;

            var right = Right.Evaluate(environment);
            if (right is Err)
                return right;

            if (left is Int && right is Int)
            {
                var leftInteger = left as Int;
                var rightInteger = right as Int;

                switch (Token.Type)
                {
                    case TokenType.Plus: return leftInteger + rightInteger;
                    case TokenType.Minus: return leftInteger - rightInteger;
                    case TokenType.Asterisk: return leftInteger * rightInteger;
                    case TokenType.Slash: return leftInteger / rightInteger;
                    case TokenType.LowerThan: return leftInteger < rightInteger;
                    case TokenType.GreaterThan: return leftInteger > rightInteger;
                    case TokenType.Equals: return leftInteger == rightInteger;
                    case TokenType.NotEquals: return leftInteger != rightInteger;
                    default:
                        return new Err("Данный инфиксный оператор не поддерживается: " +
                            $"{left.Type} {Token.Literal} {right.Type}");
                }
            }
            else if (left is Bool && right is Bool)
            {
                var leftBoolean = left as Bool;
                var rightBoolean = right as Bool;

                switch (Token.Type)
                {
                    case TokenType.Equals: return leftBoolean == rightBoolean;
                    case TokenType.NotEquals: return leftBoolean != rightBoolean;
                    default:
                        return new Err("Данный инфиксный оператор не поддерживается: " +
                            $"{left.Type} {Token.Literal} {right.Type}");
                }
            }
            else
                return new Err("Инфиксные операции над этими типами данных невозможны: " +
                    $"{left.Type} {Token.Literal} {right.Type}");
        }

        public override string ToString() =>
            $"({Left.ToString()} {Token.Literal} {Right.ToString()})";
    }
}
