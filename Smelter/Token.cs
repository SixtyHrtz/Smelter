using Smelter.Enums;
using System.Collections.Generic;

namespace Smelter
{
    public class Token
    {
        private static readonly Dictionary<string, TokenType> keywords =
            new Dictionary<string, TokenType>()
        {
            { "met", TokenType.Method },
            { "def", TokenType.Define },
            //{ "set", TokenType.Set },
            { "true", TokenType.True},
            { "false", TokenType.False },
            { "if", TokenType.If },
            { "else", TokenType.Else },
            { "ret", TokenType.Return}
        };

        public TokenType Type { get; set; }
        public string Literal { get; set; }

        public Token(TokenType type, char literal)
        {
            Type = type;
            Literal = literal.ToString();
        }

        public Token(TokenType type, string literal)
        {
            Type = type;
            Literal = literal;
        }

        public static TokenType GetIdentifier(string identifier) =>
            keywords.ContainsKey(identifier) ? keywords[identifier] : TokenType.Identifier;

        public override string ToString() => string.Format("{0,20}{1,10}", Type, Literal);
    }
}
