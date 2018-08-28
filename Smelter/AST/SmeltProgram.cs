using Smelter.Interfaces;
using Smelter.Objects;
using System.Collections.Generic;

namespace Smelter.AST
{
    public class SmeltProgram : INode
    {
        public List<IStatement> Statements { get; set; }

        public SmeltProgram() => Statements = new List<IStatement>();

        public IObj Evaluate(Environment environment)
        {
            var results = new List<IObj>();

            foreach (var statement in Statements)
            {
                var value = statement.Evaluate(environment);

                if (value is Ret)
                    return (value as Ret).Value;
                if (value is Err)
                    return value;
            }

            return Null.Ref;
        }

        public override string ToString()
        {
            var result = string.Empty;
            foreach (var statement in Statements)
                result += $"{statement}\n";
            return result;
        }
    }
}
