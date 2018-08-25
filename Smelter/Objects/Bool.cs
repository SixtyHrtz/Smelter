namespace Smelter.Objects
{
    public class Bool : IObject
    {
        public static Bool True { get; } = new Bool(true);
        public static Bool False { get; } = new Bool(false);

        public bool Value { get; }

        private Bool(bool value) => Value = value;

        public override string ToString() => Value.ToString();
    }
}
