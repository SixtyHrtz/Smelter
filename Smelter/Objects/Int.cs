namespace Smelter.Objects
{
    public class Int : IObj
    {
        public int Value { get; }

        public Int(int value) => Value = value;

        public static implicit operator Int(int value) => new Int(value);

        public static Int operator +(Int left, Int right) =>
            left.Value + right.Value;

        public static Int operator -(Int left, Int right) =>
            left.Value - right.Value;

        public static Int operator *(Int left, Int right) =>
            left.Value * right.Value;

        public static Int operator /(Int left, Int right) =>
            left.Value / right.Value;

        public static Bool operator >(Int left, Int right) =>
            left.Value > right.Value;

        public static Bool operator <(Int left, Int right) =>
            left.Value < right.Value;

        public static Bool operator ==(Int left, Int right) =>
            left.Value == right.Value;

        public static Bool operator !=(Int left, Int right) =>
            left.Value != right.Value;

        public static Int operator -(Int value) => -value.Value;

        public override string ToString() => Value.ToString();
    }
}
