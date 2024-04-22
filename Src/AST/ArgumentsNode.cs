namespace GrammarDemo.Src.AST
{
    internal class ArgumentsNode : ExpressionNode
    {
        public List<ExpressionNode> Arguments { get; private set; } = new List<ExpressionNode>();

        public void AddArgument(ExpressionNode argument)
        {
            Arguments.Add(argument);
        }
    }
}
