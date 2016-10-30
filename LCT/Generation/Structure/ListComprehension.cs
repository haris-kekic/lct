using LCT.Analysis;
using LCT.Library;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LCT.Generation.Structure
{
    public class ListComprehension
    {
        public LctUniqueList ListDefinitions { get; set; }
        public LCTGrammarParser.ListArithExpressionContext ArithmeticExpresssionContext { get; set; }
        public List<LogicOperation> LogicOperations { get; set; }
    }
}
