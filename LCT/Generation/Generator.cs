using Antlr4.Runtime.Tree;
using LCT.Generation.Preparation;
using LCT.Generation.Preparation.Intermediate;

namespace LCT.Generation
{
    internal class Generator
    {
        public Generator(IParseTree parseTree)
        {
            this.ParseTree = parseTree;
        }

        public IParseTree ParseTree { get; set; }

        public Statement Evaluate()
        {
            StatementVisitor visitor = new StatementVisitor();
            Statement statement = visitor.Visit(this.ParseTree);
            return statement;
        }
    }
}
