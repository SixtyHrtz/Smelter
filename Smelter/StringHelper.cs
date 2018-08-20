using Smelter.Interfaces;

namespace Smelter
{
    public static class StringHelper
    {
        public static string DefaultOrNull(INode node) =>
            (node == null) ? "null" : node.ToString();
    }
}
