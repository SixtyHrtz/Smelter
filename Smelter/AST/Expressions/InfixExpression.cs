using Smelter.Interfaces;
using Smelter.Objects;

namespace Smelter.AST.Expressions
{
    public class InfixExpression : IExpression
    {
        public Token Token { get; set; }
        public IExpression Left { get; set; }
        public string Operator { get; set; }
        public IExpression Right { get; set; }

        public InfixExpression(Token token, IExpression left)
        {
            Token = token;
            Operator = token.Literal;
            Left = left;
        }

        public IObject Evaluate(/*Memory memory*/)
        {
            //Object left = Left.Evaluate(memory);
            //if (left is Error)
            //    return left;

            //Object right = Right.Evaluate(memory);
            //if (right is Error)
            //    return right;

            //if (left is Integer && right is Integer)
            //{
            //    Integer leftInteger = left as Integer;
            //    Integer rightInteger = right as Integer;

            //    switch (Operator)
            //    {
            //        case Token.PLUS:
            //            return leftInteger + rightInteger;
            //        case Token.MINUS:
            //            return leftInteger - rightInteger;
            //        case Token.ASTERISK:
            //            return leftInteger * rightInteger;
            //        case Token.SLASH:
            //            return leftInteger / rightInteger;

            //        case Token.LOWER_THAN:
            //            return leftInteger < rightInteger;
            //        case Token.GREATER_THAN:
            //            return leftInteger > rightInteger;
            //        case Token.EQUALS:
            //            return leftInteger == rightInteger;
            //        case Token.NOT_EQUALS:
            //            return leftInteger != rightInteger;
            //    }
            //}
            //else if (left is Boolean && right is Boolean)
            //{
            //    Boolean leftBoolean = left as Boolean;
            //    Boolean rightBoolean = right as Boolean;

            //    switch (Operator)
            //    {
            //        case Token.EQUALS:
            //            return leftBoolean == rightBoolean;
            //        case Token.NOT_EQUALS:
            //            return leftBoolean != rightBoolean;
            //    }
            //}

            //return new Error(string.Format("Несоответствие типов: {0} {1} {2}", left.Name, Operator, right.Name));

            return null;
        }

        public override string ToString() =>
            $"({Left.ToString()} {Operator} {Right.ToString()})";
    }
}
