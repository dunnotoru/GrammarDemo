namespace GrammarDemo.Src.AST
{
    internal class BinaryOperationNode : ExpressionNode
    {
        public BinaryOperationNode(Token binary_operator, ExpressionNode leftNode, ExpressionNode rightNode)
        {
            Operator = binary_operator;
            LeftNode = leftNode;
            RightNode = rightNode;
        }

        public Token Operator { get; private set; }
        public ExpressionNode LeftNode { get; private set; }
        public ExpressionNode RightNode { get; private set; }
    }
}
