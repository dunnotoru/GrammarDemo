namespace GrammarDemo.Src.AST
{
    internal class IdentifierNode : ExpressionNode
    {
        public Token Identifier { get; private set; }
        
        public IdentifierNode(Token variable)
        {
            Identifier = variable;
        }
    }
}
