﻿using Smelter.Objects;

namespace Smelter.Interfaces
{
    public interface INode
    {
        IObj Evaluate(Environment environment);
    }
}
