namespace Smelter.Objects
{
    public class Bool : IObj
    {
        public static Bool True { get; } = new Bool(true);
        public static Bool False { get; } = new Bool(false);

        public string Type => "bool";
        public bool Value { get; }

        private Bool(bool value) => Value = value;

        public static implicit operator Bool(bool value) =>
            (value) ? True : False;

        public static Bool operator ==(Bool left, Bool right) =>
            left.Value == right.Value;

        public static Bool operator !=(Bool left, Bool right) =>
            left.Value != right.Value;

        public static Bool operator !(Bool value) => !value.Value;

        public override string ToString() => Value.ToString();
    }
}
