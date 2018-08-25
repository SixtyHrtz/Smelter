using Smelter.Interfaces;
using Smelter.Objects;
using System.Collections.Generic;

namespace Smelter
{
    public class SmeltProgram
    {
        public List<IStatement> Statements { get; set; }

        public SmeltProgram() => Statements = new List<IStatement>();

        public List<IObject> Evaluate(/*Memory memory*/)
        {
            var results = new List<IObject>();

            foreach (var statement in Statements)
            {
                //var value = statement.Evaluate(/*memory*/);

                results.Add(statement.Evaluate());

                //if (value is Return)
                //    result.Add(value.ToString());
                //if (value is Error)
                //    errors.Add(value.ToString());
            }

            return results;
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
