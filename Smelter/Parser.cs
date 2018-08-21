﻿using Smelter.AST.Expressions;
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

        public List<string> Errors { get; }

        public Parser(Lexer lexer)
        {
            this.lexer = lexer;

            Errors = new List<string>();

            prefixParseMethods = new Dictionary<TokenType, PrefixParseMethod>()
            {
                { TokenType.Identifier, ParseIdentifier }
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

        private void AddError(TokenType type)
        {
            var message = $"Ожидался {type}, но найден {nextToken.Type}.";
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
                    return ParseExpressionStatement(); ;
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

        private IStatement ParseExpressionStatement() =>
            new ExpressionStatement(token, ParseExpression(Precedence.Lowest));

        private IExpression ParseExpression(Precedence lowest)
        {
            var method = prefixParseMethods[token.Type];

            if (method == null)
            {
                //AddPrefixError(token.Type);
                return null;
            }

            var leftExpression = method();

            //while (!NextTokenIs(Token.SEMICOLON) &&
            //       precedence < NextPrecedenceIs())
            //{
            //    InfixParseMethod infix = infixParseMethods[nextToken.Type];
            //    if (infix == null)
            //        return leftExpression;

            //    NextToken();
            //    leftExpression = infix(leftExpression);
            //}

            return leftExpression;
        }

        private IExpression ParseIdentifier() => new Identifier(token);
    }
}
