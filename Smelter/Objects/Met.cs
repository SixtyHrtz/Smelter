using Smelter.AST.Expressions;
using Smelter.AST.Statements;
using System.Collections.Generic;
using System.Linq;

namespace Smelter.Objects
{
    public class Met : IObj
    {
        public Environment Environment { get; set; }
        public List<Identifier> Parameters { get; set; }
        public BlockStatement Body { get; set; }

        public string Type => "met";

        public Met(Environment environment, List<Identifier> parameters, BlockStatement body)
        {
            Environment = environment;
            Parameters = parameters;
            Body = body;
        }

        public override string ToString()
        {
            var arguments = string.Join(", ", Parameters.Select(x => x.ToString()));
            return $"met({arguments})\n{Body}\n";
        }
    }
}
