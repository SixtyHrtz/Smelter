namespace Smelter.Objects
{
    public class Err : IObj
    {
        public string Type => "err";
        public string Message { get; set; }

        public Err(string message) => Message = message;

        public override string ToString() => "Ошибка: " + Message;
    }
}
