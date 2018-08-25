﻿namespace Smelter.Objects
{
    public class Null : IObject
    {
        public static Null Ref { get; } = new Null();

        private Null() { }

        public override string ToString() => "null";
    }
}