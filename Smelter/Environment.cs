using Smelter.Objects;
using System.Collections.Generic;

namespace Smelter
{
    public class Environment
    {
        public Dictionary<string, IObj> RAM { get; set; }

        public Environment() => RAM = new Dictionary<string, IObj>();
    }
}
