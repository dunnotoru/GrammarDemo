namespace GrammarDemo.Src.AST
{
    internal class NumberNode : ExpressionNode
    {
        public NumberNode(Token number)
        {
            Number = number;
        }

        public Token Number { get; private set; }
    }
}
