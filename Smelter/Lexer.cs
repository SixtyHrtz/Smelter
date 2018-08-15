using Smelter.Enums;
using System;
using System.Collections.Generic;

namespace Smelter
{
    public class Lexer
    {
        private string input;
        private int position;
        private int nextPosition;
        private char character;

        private Dictionary<char, Func<Token>> tokenTypes;

        public Lexer(string input)
        {
            this.input = input;
            InitTokens();
            ReadChar();
        }

        private void InitTokens()
        {
            tokenTypes = new Dictionary<char, Func<Token>>();

            Register('=', () => GetEqualsToken());
            Register('+', TokenType.Plus);
            Register('-', TokenType.Minus);
            Register('*', TokenType.Asterisk);
            Register('/', TokenType.Slash);
            Register('!', () => GetBangToken());
            Register('>', TokenType.GreaterThan);
            Register('<', TokenType.LowerThan);
            Register(',', TokenType.Comma);
            Register(';', TokenType.Semicolon);
            Register('(', TokenType.LeftParenthesis);
            Register(')', TokenType.RightParenthesis);
            Register('{', TokenType.LeftBrace);
            Register('}', TokenType.RightBrace);
            Register('\0', TokenType.EndOfFile);
        }

        private void Register(char ch, Func<Token> func) =>
            tokenTypes.Add(ch, func);

        private void Register(char ch, TokenType tokenType) =>
            tokenTypes.Add(ch, () => { return new Token(tokenType, ch); });

        private Token GetEqualsToken()
        {
            if (GetNextChar() != '=')
                return new Token(TokenType.Assign, character);
            return new Token(TokenType.Equals, character.ToString() + ReadChar());
        }

        private Token GetBangToken()
        {
            if (GetNextChar() != '=')
                return new Token(TokenType.Bang, character);
            return new Token(TokenType.NotEquals, character.ToString() + ReadChar());
        }

        private char GetNextChar() =>
            (nextPosition >= input.Length) ? '\0' : input[nextPosition];

        private char ReadChar()
        {
            character = (nextPosition >= input.Length) ? '\0' : input[nextPosition];
            position = nextPosition++;
            return character;
        }

        public Token NextToken()
        {
            SkipWhiteSpaces();
            Token token;

            if (tokenTypes.ContainsKey(character))
                token = tokenTypes[character]();
            else
            {
                if (IsLetter())
                    return GetIdentifierToken();
                else if (IsDigit())
                    return new Token(TokenType.Integer, ReadNumber());
                else
                    token = new Token(TokenType.Unknown, character);
            }

            ReadChar();
            return token;
        }

        private void SkipWhiteSpaces()
        {
            while (character == ' ' || character == '\t' ||
                character == '\n' || character == '\r')
                ReadChar();
        }

        private Token GetIdentifierToken()
        {
            var literal = ReadIdentifier();
            var identifier = Token.GetIdentifier(literal);
            return new Token(identifier, literal);
        }

        private bool IsLetter() => character == '_' ||
            (character >= 'a' && character <= 'z') ||
            (character >= 'A' && character <= 'Z');


        private string ReadIdentifier()
        {
            var pos = position;
            while (IsLetter())
                ReadChar();

            return input.Substring(pos, position - pos);
        }

        private bool IsDigit() => character >= '0' && character <= '9';

        private string ReadNumber()
        {
            var pos = position;
            while (IsDigit())
                ReadChar();

            return input.Substring(pos, position - pos);
        }
    }
}
