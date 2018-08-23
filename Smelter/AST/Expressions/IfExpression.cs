using Smelter.AST.Statements;
using Smelter.Interfaces;

namespace Smelter.AST.Expressions
{
    public class IfExpression : IExpression
    {
        public Token Token { get; set; }
        public IExpression Condition { get; set; }
        public BlockStatement Consequence { get; set; }
        public BlockStatement Alternative { get; set; }

        public IfExpression(Token token) => Token = token;

        //public Object Evaluate(Memory memory)
        //{
        //    Object value = Condition.Evaluate(memory);
        //    if (value is Error)
        //        return value;

        //    if (value is Boolean)
        //    {
        //        Boolean booleanValue = value as Boolean;
        //        if (booleanValue.Value)
        //            return Consequence.Evaluate(memory);
        //        else if (Alternative != null)
        //            return Alternative.Evaluate(memory);
        //    }

        //    return null;
        //}

        public override string ToString()
        {
            var result = $"if {Condition} {Consequence}";
            if (Alternative != null)
                result += $"else {Alternative}";

            return result;
        }
    }
}
