using Smelter.Objects;
using System.Collections.Generic;

namespace Smelter
{
    public class Environment
    {
        private readonly Dictionary<string, IObj> variables;

        public Environment OuterEnvironment { get; set; }

        public Environment() => variables = new Dictionary<string, IObj>();

        public Environment(Met method, List<IObj> arguments)
        {
            OuterEnvironment = method.Environment;

            variables = new Dictionary<string, IObj>();
            for (int i = 0; i < method.Parameters.Count; i++)
                variables[method.Parameters[i].Value] = arguments[i];
        }

        public bool ContainsVariable(string name) => variables.ContainsKey(name);

        public IObj GetVariable(string name)
        {
            if (variables.ContainsKey(name))
                return variables[name];
            return (OuterEnvironment == null) ? Null.Ref : OuterEnvironment.GetVariable(name);
        }

        public void SetVariable(string name, IObj value) => variables[name] = value;
    }
}
