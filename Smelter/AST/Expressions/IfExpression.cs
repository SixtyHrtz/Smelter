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

        public IObj Evaluate(/*Memory memory*/)
        {
            var condition = Condition.Evaluate(/*memory*/);
            //if (value is Error)
            //    return value;

            if (condition is Bool)
            {
                var value = condition as Bool;

                if (value.Value)
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
