namespace Smelter.Objects
{
    public class Str : IObj
    {
        public string Type => "str";
        public string Value { get; }

        public Str(string value) => Value = value;

        public static implicit operator Str(string value) => new Str(value);

        public static Str operator +(Str left, Str right) =>
            left.Value + right.Value;

        public static Bool operator ==(Str left, Str right) =>
            left.Value == right.Value;

        public static Bool operator !=(Str left, Str right) =>
            left.Value != right.Value;

        public override string ToString() => Value;
    }
}
