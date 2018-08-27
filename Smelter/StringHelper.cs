namespace Smelter
{
    public static class StringHelper
    {
        public static string DefaultOrNull(object obj) =>
            (obj == null) ? "null" : obj.ToString();
    }
}
