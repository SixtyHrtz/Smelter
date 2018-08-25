namespace Smelter.Objects
{
    public class Int : IObject
    {
        public int Value { get; }

        public Int(int value) => Value = value;

        public override string ToString() => Value.ToString();
    }
}
