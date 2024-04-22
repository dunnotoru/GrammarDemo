﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GrammarDemo.Src
{
    internal class Lexer
    {
        public IEnumerable<Token> Scan(string code)
        {
            if (code.Length == 0)
            {
                return Enumerable.Empty<Token>();
            }

            List<Token> tokens = new List<Token>();
            int position = 0;

            code = code.Replace("\t", "").Replace("\r", "");

            do
            {
                string rawToken = ParseToken(code, position);
                tokens.Add(new Token(rawToken, position));
                position += rawToken.Length;

            } while (position < code.Length);

            List<Token> resultTokens = new List<Token>();
            for (int i = 0; i < tokens.Count - 1; i++)
            {
                if ((tokens[i].Type == TokenType.Plus || tokens[i].Type == TokenType.Minus)
                    && (tokens[i + 1].Type == TokenType.DoubleLiteral || tokens[i + 1].Type == TokenType.IntegerLiteral))
                {
                    string rawNumber = tokens[i].RawToken + tokens[i + 1].RawToken;
                    Token numberToken = new Token(rawNumber, tokens[i].StartPos);
                    resultTokens.Add(numberToken);
                    i++;
                }
                else
                {
                    resultTokens.Add(tokens[i]);
                }
            }
            resultTokens.Add(tokens.Last());

            return resultTokens;
        }

        private string ParseToken(string code, int position)
        {
            char symbol = code[position];
            string allowedIdentifierSymbols = "_:<>";
            if (char.IsWhiteSpace(symbol))
            {
                return symbol.ToString();
            }
            if (char.IsLetter(symbol) || symbol == '_')
            {
                return Parse(code, position, (c) => !char.IsLetterOrDigit(c) && !allowedIdentifierSymbols.Contains(c));
            }
            if (char.IsDigit(symbol))
            {
                return Parse(code, position, (c) => !char.IsLetterOrDigit(c) && c != '.');
            }

            return ParseOperator(code, position);
        }

        private string Parse(string code, int position, Func<char, bool> stopRule)
        {
            char symbol;
            StringBuilder buffer = new StringBuilder();
            while (position < code.Length)
            {
                symbol = code[position];
                if (stopRule(symbol))
                {
                    break;
                }

                buffer.Append(symbol);
                position++;
            }

            return buffer.ToString();
        }

        private string ParseOperator(string code, int position)
        {
            string symbol = code[position].ToString();

            string firstCharacter = "<>=&!|";
            string secondCharacter = "=&|";
            if (position < code.Length - 1)
            {
                if (firstCharacter.Contains(symbol) && secondCharacter.Contains(code[position + 1]))
                {
                    symbol += code[position + 1];
                }
            }

            return symbol;
        }
    }
}
