using GrammarDemo.Src.AST;

namespace GrammarDemo.Src
{
    internal class Parser
    {
        private int _position;
        private List<Token> _tokens = new List<Token>();

        public Parser(List<Token> tokens)
        {
            _position = 0;
            _tokens = tokens;
        }

        public bool Match(out Token? token, params TokenType[] expectedType)
        {
            token = null;
            if (_position < _tokens.Count)
            {
                Token currentToken = _tokens[_position];
                if (expectedType.Any(_ => _ == currentToken.Type))
                {
                    _position++;
                    token = currentToken;
                    return true;
                }
            }

            return false;
        }

        public bool Requre(out Token? token, params TokenType[] expectedType)
        {
            if (Match(out token, expectedType) == false)
            {
                return false;
            }

            return true;
        }

        public ExpressionNode Parse()
        {
            _position = 0;
            StatementsNode root = new StatementsNode();
            while (_position < _tokens.Count)
            {
                ExpressionNode lineNode = ParseStatement();
                root.AddNode(lineNode);
            }

            return root;
        }

        private ExpressionNode? ParseStatement()
        {
            if (TryParseDeclaration(out ExpressionNode? expressionNode))
            {
                return expressionNode;
            }
            _position--;
            if (TryParseExpression(out expressionNode))
            {
                return expressionNode;
            }

            throw new Exception("cant parse");
        }

        private bool TryParseExpression(out ExpressionNode? expressionNode)
        {
                expressionNode = null;
            if (Match(out Token? idToken, TokenType.Identifier))
            {
                if (Match(out Token assignmentOp, TokenType.Assignment))
                {
                    TryParseFormula(out ExpressionNode? rightNode);
                    ExpressionNode LeftNode = new IdentifierNode(idToken);
                    if (Match(out _, TokenType.Semicolon))
                    {
                        expressionNode = new BinaryOperationNode(assignmentOp, LeftNode, rightNode);
                        return true;
                    }
                }
            }

            return false;
        }

        private bool TryParseIdentificatorOrConstant(out ExpressionNode? expressionNode)
        {
            expressionNode = null;
            if (Match(out Token? number, TokenType.IntegerLiteral))
            {
                expressionNode = new NumberNode(number);
                return true;
            }
            if (Match(out Token? id, TokenType.Identifier))
            {
                expressionNode = new IdentifierNode(id);
                return true;
            }

            return false;
        }

        private bool TryParseParenthesis(out ExpressionNode? expressionNode)
        {
            expressionNode = null;
            if (Match(out _, TokenType.OpenParenthesis))
            {
                if (TryParseFormula(out ExpressionNode? innerFormula))
                {
                    if (Match(out _, TokenType.CloseParenthesis))
                    {
                        expressionNode = innerFormula;
                        return true;
                    }
                }
            }
            else
            {
                if (TryParseIdentificatorOrConstant(out ExpressionNode? valueNode))
                {
                    expressionNode = valueNode;
                    return true;
                }
            }

            return false;
        }

        private bool TryParseFormula(out ExpressionNode? expressionNode)
        {
            TryParseParenthesis(out ExpressionNode? leftNode);
            while(Match(out Token? op, TokenType.Plus, TokenType.Minus))
            {
                TryParseParenthesis(out ExpressionNode? rightNode);
                leftNode = new BinaryOperationNode(op, leftNode, rightNode);
            }

            expressionNode = leftNode;
            return true;
        }

        private bool TryParseDeclaration(out ExpressionNode? expressionNode)
        {
            expressionNode = null;
            if (Match(out Token? typeToken, TokenType.Identifier, TokenType.Integer, TokenType.Double, TokenType.Complex))
            {
                if (Match(out Token? IdToken, TokenType.Identifier))
                {
                    if (Match(out _, TokenType.Semicolon))
                    {
                        expressionNode = new DeclarationNode(typeToken, IdToken);
                        return true;
                    }
                    else if (Match(out Token assignmentOp, TokenType.Assignment))
                    {
                        TryParseFormula(out ExpressionNode? rightNode);
                        if (Match(out _, TokenType.Semicolon))
                        {
                            ExpressionNode LeftNode = new DeclarationNode(typeToken, IdToken);
                            expressionNode = new BinaryOperationNode(assignmentOp, LeftNode, rightNode);
                            return true;
                        }
                    }
                }
            }

            return false;
        }
    }
}
