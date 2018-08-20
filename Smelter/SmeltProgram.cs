using Smelter.Interfaces;
using System.Collections.Generic;

namespace Smelter
{
    public class SmeltProgram
    {
        public List<IStatement> Statements { get; set; }

        public SmeltProgram() => Statements = new List<IStatement>();

        public override string ToString()
        {
            var result = string.Empty;
            foreach (var statement in Statements)
                result += $"{statement}\n";
            return result;
        }
    }
}
