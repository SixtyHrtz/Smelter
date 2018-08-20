using Smelter.Interfaces;

namespace Smelter.AST.Statements
{
    public class RetStatement : IStatement
    {
        public Token Token { get; set; }
        public IExpression Value { get; set; }

        public RetStatement(Token token) => Token = token;

        //public Object Evaluate(Memory memory)
        //{
        //    Object value = Value.Evaluate(memory);
        //    if (value is Error)
        //        return value;

        //    return new Return(value);
        //}

        public override string ToString()
        {
            string value = (Value == null) ? "null" : Value.ToString();
            return $"[{Token} = {value}]";
        }
    }
}
