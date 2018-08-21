using Smelter.Interfaces;

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

        //public Object Evaluate(Memory memory)
        //{
        //    Object right = Expression.Evaluate(memory);

        //    if (right is Error)
        //        return right;

        //    switch (Operator)
        //    {
        //        case "!":
        //            if (right is Boolean)
        //                return !(right as Boolean);
        //            break;
        //        case "-":
        //            if (right is Integer)
        //                return -(right as Integer);
        //            break;
        //    }

        //    return new Error(string.Format("Неизвестный оператор: {0}{1}", Operator, right.Name));
        //}

        public override string ToString() =>
            $"({Operator}{Right.ToString()})";
    }
}
