using Antlr4.Runtime.Tree;
using LCT.Generation.Structure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LCT.Generation
{
    public class DefaultGenerator : IGenerator
    {
        public InterpretationUnit Generate(IParseTree parseTree)
        {
            StatementVisitor visitor = new StatementVisitor();
            return visitor.Visit(parseTree);
        }
    }
}
