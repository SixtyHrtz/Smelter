using Smelter.Enums;
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

                switch (Operator)
                {
                    case "+":
                        return leftInteger + rightInteger;
                    case "-":
                        return leftInteger - rightInteger;
                    case "*":
                        return leftInteger * rightInteger;
                    case "/":
                        return leftInteger / rightInteger;

                    case "<":
                        return leftInteger < rightInteger;
                    case ">":
                        return leftInteger > rightInteger;
                    case "==":
                        return leftInteger == rightInteger;
                    case "!=":
                        return leftInteger != rightInteger;
                }
            }
            else if (left is Bool && right is Bool)
            {
                var leftBoolean = left as Bool;
                var rightBoolean = right as Bool;

                switch (Operator)
                {
                    case "==":
                        return leftBoolean == rightBoolean;
                    case "!=":
                        return leftBoolean != rightBoolean;
                }
            }

            //return new Error(string.Format("Несоответствие типов: {0} {1} {2}", left.Name, Operator, right.Name));
            return Null.Ref;
        }

        public override string ToString() =>
            $"({Left.ToString()} {Operator} {Right.ToString()})";
    }
}
