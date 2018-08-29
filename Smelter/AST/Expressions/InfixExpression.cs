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
                var leftInt = left as Int;
                var rightInt = right as Int;

                switch (Token.Type)
                {
                    case TokenType.Plus: return leftInt + rightInt;
                    case TokenType.Minus: return leftInt - rightInt;
                    case TokenType.Asterisk: return leftInt * rightInt;
                    case TokenType.Slash: return leftInt / rightInt;
                    case TokenType.GreaterThan: return leftInt > rightInt;
                    case TokenType.LowerThan: return leftInt < rightInt;
                    case TokenType.Equals: return leftInt == rightInt;
                    case TokenType.NotEquals: return leftInt != rightInt;
                    default:
                        return new Err("Данный инфиксный оператор не поддерживается: " +
                            $"{left.Type} {Token.Literal} {right.Type}");
                }
            }
            else if (left is Bool && right is Bool)
            {
                var leftBool = left as Bool;
                var rightBool = right as Bool;

                switch (Token.Type)
                {
                    case TokenType.Equals: return leftBool == rightBool;
                    case TokenType.NotEquals: return leftBool != rightBool;
                    default:
                        return new Err("Данный инфиксный оператор не поддерживается: " +
                            $"{left.Type} {Token.Literal} {right.Type}");
                }
            }
            else if (left is Str && right is Str)
            {
                var leftStr = left as Str;
                var rightStr = right as Str;

                switch (Token.Type)
                {
                    case TokenType.Plus: return leftStr + rightStr;
                    case TokenType.Equals: return leftStr == rightStr;
                    case TokenType.NotEquals: return leftStr != rightStr;
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
