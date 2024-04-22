namespace GrammarDemo.Src.AST
{
    internal class DeclarationNode : ExpressionNode
    {
        public DeclarationNode(Token typeToken, Token idToken)
        {
            TypeToken = typeToken;
            IdToken = idToken;
        }

        public Token TypeToken { get; private set; }
        public Token IdToken { get; private set; }
    }
}
