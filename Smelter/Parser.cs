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
                { TokenType.Minus, ParsePrefixExpression },
                { TokenType.LeftParenthesis, ParseGroupedExpression },
                { TokenType.If, ParseIfExpression },
                { TokenType.Method, ParseMetLiteral }
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
                { TokenType.LowerThan, ParseInfixExpression },
                { TokenType.LeftParenthesis, ParseCallExpression }
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
                { TokenType.LeftParenthesis, Precedence.Call }
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

        private IExpression ParseGroupedExpression()
        {
            NextToken();
            var expression = ParseExpression(Precedence.Lowest);

            if (!NextToken(TokenType.RightParenthesis))
                return null;

            return expression;
        }

        private IExpression ParseIfExpression()
        {
            var expression = new IfExpression(token);
            if (!NextToken(TokenType.LeftParenthesis))
                return null;

            NextToken();
            expression.Condition = ParseExpression(Precedence.Lowest);

            if (!NextToken(TokenType.RightParenthesis))
                return null;

            if (!NextToken(TokenType.LeftBrace))
                return null;

            expression.Consequence = ParseBlockStatement();

            if (NextTokenIs(TokenType.Else))
            {
                NextToken();
                if (!NextToken(TokenType.LeftBrace))
                    return null;

                expression.Alternative = ParseBlockStatement();
            }

            return expression;
        }


        private BlockStatement ParseBlockStatement()
        {
            var blockStatement = new BlockStatement(token);
            NextToken();

            while (!CurrentTokenIs(TokenType.RightBrace))
            {
                var statement = ParseStatement();
                if (statement != null)
                    blockStatement.Statements.Add(statement);

                NextToken();
            }

            return blockStatement;
        }

        private IExpression ParseMetLiteral()
        {
            var literal = new MetLiteral(token);

            if (!NextToken(TokenType.LeftParenthesis))
                return null;

            literal.Parameters = ParseMethodParameters();

            if (!NextToken(TokenType.LeftBrace))
                return null;

            literal.Body = ParseBlockStatement();
            return literal;
        }

        private List<Identifier> ParseMethodParameters()
        {
            var identifiers = new List<Identifier>();

            if (NextTokenIs(TokenType.RightParenthesis))
            {
                NextToken();
                return identifiers;
            }

            NextToken();
            identifiers.Add(new Identifier(token));

            while (NextTokenIs(TokenType.Comma))
            {
                NextToken();
                NextToken();
                identifiers.Add(new Identifier(token));
            }

            if (!NextToken(TokenType.RightParenthesis))
                return null;

            return identifiers;
        }

        private CallExpression ParseCallExpression(IExpression expression) =>
            new CallExpression(token, expression, ParseCallArguments());

        private List<IExpression> ParseCallArguments()
        {
            var arguments = new List<IExpression>();

            if (NextTokenIs(TokenType.LeftParenthesis))
            {
                NextToken();
                return arguments;
            }

            NextToken();
            arguments.Add(ParseExpression(Precedence.Lowest));

            while (NextTokenIs(TokenType.Comma))
            {
                NextToken();
                NextToken();
                arguments.Add(ParseExpression(Precedence.Lowest));
            }

            if (!NextToken(TokenType.RightParenthesis))
                return null;

            return arguments;
        }
    }
}
