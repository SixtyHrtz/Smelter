using Smelter.AST.Statements;
using Smelter.Interfaces;
using Smelter.Objects;

namespace Smelter.AST.Expressions
{
    public class IfExpression : IExpression
    {
        public Token Token { get; set; }
        public IExpression Condition { get; set; }
        public BlockStatement Consequence { get; set; }
        public BlockStatement Alternative { get; set; }

        public IfExpression(Token token) => Token = token;

        public IObject Evaluate(/*Memory memory*/)
        {
            var value = Condition.Evaluate(/*memory*/);
            //if (value is Error)
            //    return value;

            if (value is Bool)
            {
                var boolValue = value as Bool;

                if (boolValue.Value)
                    return Consequence.Evaluate(/*memory*/);
                else if (Alternative != null)
                    return Alternative.Evaluate(/*memory*/);
            }

            return Null.Ref;
        }

        public override string ToString()
        {
            var result = $"if {Condition}\n{Consequence}";
            if (Alternative != null)
                result += $"else\n{Alternative}";

            return result;
        }
    }
}
