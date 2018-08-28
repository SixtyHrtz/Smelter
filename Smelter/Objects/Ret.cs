namespace Smelter.Objects
{
    public class Ret : IObj
    {
        public string Type => "ret";
        public IObj Value { get; set; }

        public Ret(IObj value) => Value = value;

        public override string ToString() => StringHelper.DefaultOrNull(Value);
    }
}
