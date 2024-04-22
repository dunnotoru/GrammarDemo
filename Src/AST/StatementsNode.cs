namespace GrammarDemo.Src.AST
{
    internal class StatementsNode : ExpressionNode
    {
        public List<ExpressionNode> Lines { get; private set; } = new List<ExpressionNode>();

        public void AddNode(ExpressionNode token)
        {
            Lines.Add(token);
        }
    }
}
