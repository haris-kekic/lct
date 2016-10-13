using LCT.Analysis;
using LCT.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LCT.Generation.Preparation.Intermediate
{
    public class ListComprehension : IntermediateCode
    {
        public LctUniqueList ListDefinitions { get; set; }
        public LCTGrammarParser.ListArithExpressionContext ArithmeticExpresssionContext { get; set; }
    }
}
