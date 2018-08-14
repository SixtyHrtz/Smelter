namespace Smelter.Enums
{
    public enum TokenType
    {
        Unknown,
        EndOfFile,

        Identifier,
        Integer,

        Assign,
        Plus,
        Minus,
        Asterisk,
        Slash,

        Bang,
        GreaterThan,
        LowerThan,
        Equals,
        NotEquals,
        Comma,
        Semicolon,
        LeftParenthesis,
        RightParenthesis,
        LeftBrace,
        RightBrace,

        Method,
        Define,

        True,
        False,
        If,
        Else,
        Return
    }
}
