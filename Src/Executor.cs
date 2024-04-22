using GrammarDemo.Src.AST;

namespace GrammarDemo.Src
{
    internal class Executor
    {
        public void Execute(ExpressionNode node)
        {

        }

        public void Print(ExpressionNode node)
        {
            if (node is StatementsNode)
            {
                var t = node as StatementsNode;
                foreach (var line in t.Lines)
                {
                    Print(line);
                }
                return;
            }
            if(node is IdentifierNode)
            {
                Console.WriteLine((node as IdentifierNode).Identifier.RawToken);
                return;
            }
            if (node is NumberNode)
            {
                Console.WriteLine((node as NumberNode).Number.RawToken);
                return;
            }
            if (node is DeclarationNode)
            {
                var t = node as DeclarationNode;
                Console.WriteLine($"{t.TypeToken.RawToken} {t.IdToken.RawToken}");
            }
            if (node is BinaryOperationNode)
            {
                var t = node as BinaryOperationNode;
                Print(t.LeftNode);
                Console.WriteLine(t.Operator.RawToken);
                Print(t.RightNode);
            }
        }
    }
}
