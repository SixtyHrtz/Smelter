using Smelter.AST.Expressions;
using Smelter.AST.Statements;
using Smelter.Enums;
using Smelter.Interfaces;
using System.Collections.Generic;

namespace Smelter
{
    public class Parser
    {
        delegate IExpression PrefixParseMethod();
        delegate IExpression InfixParseMethod(IExpression expression);

        private Lexer lexer;

        private Token token;
        private Token nextToken;

        private readonly Dictionary<TokenType, PrefixParseMethod> prefixParseMethods;
        private readonly Dictionary<TokenType, InfixParseMethod> infixParseMethods;

        private readonly Dictionary<TokenType, Precedence> precedences;

        public List<string> Errors { get; }

        public Parser(Lexer lexer)
        {
            this.lexer = lexer;

            Errors = new List<string>();

            prefixParseMethods = new Dictionary<TokenType, PrefixParseMethod>()
            {
                { TokenType.Identifier, ParseIdentifier },
                { TokenType.Integer, ParseIntLiteral },
                { TokenType.True, ParseBoolLiteral },
                { TokenType.False, ParseBoolLiteral },
                { TokenType.Bang, ParsePrefixExpression },
                { TokenType.Minus, ParsePrefixExpression }
            };

            infixParseMethods = new Dictionary<TokenType, InfixParseMethod>()
            {
                { TokenType.Plus, ParseInfixExpression },
                { TokenType.Minus, ParseInfixExpression },
                { TokenType.Asterisk, ParseInfixExpression },
                { TokenType.Slash, ParseInfixExpression },
                { TokenType.Equals, ParseInfixExpression },
                { TokenType.NotEquals, ParseInfixExpression },
                { TokenType.GreaterThan, ParseInfixExpression },
                { TokenType.LowerThan, ParseInfixExpression }
            };

            precedences = new Dictionary<TokenType, Precedence>()
            {
                { TokenType.Equals, Precedence.Equals },
                { TokenType.NotEquals, Precedence.Equals },
                { TokenType.GreaterThan, Precedence.LessOrGreater },
                { TokenType.LowerThan, Precedence.LessOrGreater },
                { TokenType.Plus, Precedence.Sum },
                { TokenType.Minus, Precedence.Sum },
                { TokenType.Slash, Precedence.Product },
                { TokenType.Asterisk, Precedence.Product },
                //{ TokenType.LeftParenthesis, Precedence.Call }
            };

            NextToken();
            NextToken();
        }

        private bool CurrentTokenIs(TokenType type) => token.Type == type;
        private bool NextTokenIs(TokenType type) => nextToken.Type == type;

        private void NextToken()
        {
            token = nextToken;
            nextToken = lexer.NextToken();
        }

        private bool NextToken(TokenType type)
        {
            if (NextTokenIs(type))
            {
                NextToken();
                return true;
            }
            else
            {
                AddError(type);
                return false;
            }
        }

        private Precedence NextPrecedenceIs()
        {
            if (precedences.ContainsKey(nextToken.Type))
                return precedences[nextToken.Type];
            else
                return Precedence.Lowest;
        }

        private Precedence CurrentPrecedence()
        {
            if (precedences.ContainsKey(token.Type))
                return precedences[token.Type];
            else
                return Precedence.Lowest;
        }

        private void AddError(string message) => Errors.Add(message);

        private void AddError(TokenType type)
        {
            var message = $"Ожидался {type}, но найден {nextToken.Type}.";
            Errors.Add(message);
        }

        private void AddPrefixMethodError(TokenType type)
        {
            var message = $"Для токена {type} не найдены подходящие префиксные методы!";
            Errors.Add(message);
        }

        public SmeltProgram Parse()
        {
            var program = new SmeltProgram();

            while (!CurrentTokenIs(TokenType.EndOfFile))
            {
                var statement = ParseStatement();
                if (statement != null)
                    program.Statements.Add(statement);

                NextToken();
            }

            return program;
        }

        private IStatement ParseStatement()
        {
            switch (token.Type)
            {
                case TokenType.Define:
                    return ParseDefStatement();
                case TokenType.Return:
                    return ParseRetStatement();
                default:
                    return ParseExpressionStatement();
            }
        }

        private IStatement ParseDefStatement()
        {
            var statement = new DefStatement(token);

            if (!NextToken(TokenType.Identifier))
                return null;

            statement.Name = new Identifier(token);

            if (!NextToken(TokenType.Assign))
                return null;

            // TODO
            while (!CurrentTokenIs(TokenType.Semicolon))
                NextToken();

            //NextToken();
            //statement.Value = ParseExpression(Precedence.Lowest);

            //if (!NextToken(TokenType.Semicolon))
            //    return null;

            return statement;
        }

        private IStatement ParseRetStatement()
        {
            var statement = new RetStatement(token);

            NextToken();

            // TODO
            while (!CurrentTokenIs(TokenType.Semicolon))
                NextToken();

            //statement.Value = ParseExpression(Precedence.Lowest);

            //if (!NextToken(TokenType.Semicolon))
            //    return null;

            return statement;
        }

        private IStatement ParseExpressionStatement()
        {
            var statement = new ExpressionStatement(token, ParseExpression(Precedence.Lowest));

            if (NextTokenIs(TokenType.Semicolon))
                NextToken();

            return statement;
        }


        private IExpression ParseExpression(Precedence precedence)
        {
            var prefixMethod = prefixParseMethods[token.Type];

            if (prefixMethod == null)
            {
                AddPrefixMethodError(token.Type);
                return null;
            }

            var leftExpression = prefixMethod();

            while (!NextTokenIs(TokenType.Semicolon) && precedence < NextPrecedenceIs())
            {
                var infixMethod = infixParseMethods[nextToken.Type];
                if (infixMethod == null)
                    return leftExpression;

                NextToken();
                leftExpression = infixMethod(leftExpression);
            }

            return leftExpression;
        }

        private IExpression ParseIdentifier() => new Identifier(token);

        private IExpression ParseIntLiteral()
        {
            var literal = new IntLiteral(token);

            if (!int.TryParse(token.Literal, out int value))
            {
                string msg = $"Не удалось привести {token.Literal} к целому числу.";
                AddError(msg);
                return null;
            }

            literal.Value = value;
            return literal;
        }

        private IExpression ParseBoolLiteral()
        {
            var literal = new BoolLiteral(token);

            if (!bool.TryParse(token.Literal, out bool value))
            {
                string msg = $"Не удалось привести {token.Literal} к логическому типу.";
                AddError(msg);
                return null;
            }

            literal.Value = value;
            return literal;
        }

        private IExpression ParsePrefixExpression()
        {
            var expression = new PrefixExpression(token);

            NextToken();
            expression.Right = ParseExpression(Precedence.Prefix);

            return expression;
        }

        private IExpression ParseInfixExpression(IExpression left)
        {
            var expression = new InfixExpression(token, left);

            var precedence = CurrentPrecedence();
            NextToken();
            expression.Right = ParseExpression(precedence);

            return expression;
        }
    }
}
