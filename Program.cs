using GrammarDemo.Src;
using GrammarDemo.Src.AST;

namespace GrammarDemo
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string code = "a b = 1 + 4 + 6 + 7;";
            Lexer lexer = new Lexer();
            List<Token> tokens = lexer.Scan(code).ToList();
            tokens.RemoveAll(_ => string.IsNullOrWhiteSpace(_.RawToken));
            Parser parser = new Parser(tokens);
            ExpressionNode expressionNode = parser.Parse();
            Executor executor = new Executor();
            executor.Print((expressionNode as StatementsNode).Lines[0]);
        }
    }
}
