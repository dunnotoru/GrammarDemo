namespace GrammarDemo.Src.AST
{
    internal class FunctionNode : ExpressionNode
    {
        public FunctionNode(IdentifierNode identifier, ArgumentsNode arguments)
        {
            Identifier = identifier;
            Arguments = arguments;
        }

        public IdentifierNode Identifier { get; private set; }
        public ArgumentsNode Arguments { get; private set; }
    }
}
