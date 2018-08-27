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

        public IObj Evaluate(/*Memory memory*/)
        {
            var left = Left.Evaluate(/*memory*/);
            //if (left is Error)
            //    return left;

            var right = Right.Evaluate(/*memory*/);
            //if (right is Error)
            //    return right;

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
                }
            }

            //return new Error(string.Format("Несоответствие типов: {0} {1} {2}", left.Name, Operator, right.Name));
            return Null.Ref;
        }

        public override string ToString() =>
            $"({Left.ToString()} {Token.Type} {Right.ToString()})";
    }
}
