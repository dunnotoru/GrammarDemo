﻿using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text.RegularExpressions;

namespace GrammarDemo.Src
{
    internal enum TokenType
    {
        Identifier = 0,
        Complex,
        Integer,
        Double,
        Whitespace,
        Less,
        Greater,
        OpenParenthesis,
        CloseParenthesis,
        IntegerLiteral,
        DoubleLiteral,
        Comma,
        Semicolon,

        Newline,
        Assignment,

        Plus,
        Minus,
        Multiply,
        Divide,
        Module,

        Invalid
    }

    internal class Token
    {
        public static Dictionary<string, TokenType> DefaultTypes { get; }
            = new Dictionary<string, TokenType>()
        {
            { "int", TokenType.Integer },
            { "double", TokenType.Double },
            { "std::complex<double>", TokenType.Complex },

            { "\n", TokenType.Newline },
            { " ", TokenType.Whitespace },
            { ",", TokenType.Comma },
            { ";", TokenType.Semicolon },
            { "=", TokenType.Assignment },

            { "(", TokenType.OpenParenthesis },
            { ")", TokenType.CloseParenthesis },

            { "+", TokenType.Plus },
            { "-", TokenType.Minus },
            { "*", TokenType.Multiply },
            { "/", TokenType.Divide },
            { "%", TokenType.Module },

            { ">", TokenType.Greater},
            { "<", TokenType.Less },
        };

        public TokenType Type { get; }
        public string RawToken { get; }
        public int StartPos { get; }
        public int EndPos { get => StartPos + RawToken.Length; }

        public Token(string rawToken, int startPos)
        {
            if (rawToken.Length == 0)
                throw new ArgumentException("raw token is empty");

            RawToken = rawToken;
            StartPos = startPos;
            Type = GetTokenType(rawToken);
        }

        public static bool DefaultTokenExists(string rawToken)
            => DefaultTypes.ContainsKey(rawToken);

        private static bool IsIdentifier(string rawToken)
        {
            if (rawToken == "std::complex<double>")
                return false;

            return rawToken.Length != 0 && (char.IsLetter(rawToken.First()) || rawToken.First() == '_') && Regex.IsMatch(rawToken, "^[a-zA-Z0-9_]+$");
        }
        private static bool IsIntegerLiteral(string rawToken)
            => int.TryParse(rawToken, out int _) && !rawToken.StartsWith("0.");
        private static bool IsDoubleLiteral(string rawToken)
        {
            return double.TryParse(rawToken, NumberFormatInfo.InvariantInfo, out double _)
                && rawToken.StartsWith("0.") != !rawToken.StartsWith('0')
                && !rawToken.EndsWith('.');
        }
        private static bool IsStringLiteral(string rawToken)
            => rawToken.StartsWith("\"") && rawToken.EndsWith("\"") && !rawToken.Contains('\n') && rawToken.Length > 1;

        public static TokenType GetTokenType(string rawToken)
        {
            if (DefaultTokenExists(rawToken))
            {
                return DefaultTypes[rawToken];
            }
            if (IsIdentifier(rawToken))
            {
                return TokenType.Identifier;
            }
            if (IsIntegerLiteral(rawToken))
            {
                return TokenType.IntegerLiteral;
            }
            if (IsDoubleLiteral(rawToken))
            {
                return TokenType.DoubleLiteral;
            }

            return TokenType.Invalid;
        }
    }
}
