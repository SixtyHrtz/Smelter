﻿using Smelter.Interfaces;
using Smelter.Objects;

namespace Smelter.AST.Expressions
{
    public class IntLiteral : IExpression
    {
        public Token Token { get; set; }
        public int Value { get; set; }

        public IntLiteral(Token token, int value)
        {
            Token = token;
            Value = value;
        }

        public IObj Evaluate(Environment environment) => new Int(Value);

        public override string ToString() => Token.Literal;
    }
}
