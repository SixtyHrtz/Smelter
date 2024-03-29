﻿using Smelter.AST.Statements;
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

        public IObj Evaluate(Environment environment)
        {
            var condition = Condition.Evaluate(environment);
            if (condition is Err)
                return condition;

            if (condition is Bool)
            {
                var value = condition as Bool;

                if (value.Value)
                    return Consequence.Evaluate(environment);
                else if (Alternative != null)
                    return Alternative.Evaluate(environment);
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
