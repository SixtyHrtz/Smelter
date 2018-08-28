namespace Smelter.Objects
{
    public class Null : IObj
    {
        public static Null Ref { get; } = new Null();

        public string Type => "null";

        private Null() { }

        public override string ToString() => "null";
    }
}
