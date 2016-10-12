using Antlr4.Runtime;
using Antlr4.Runtime.Tree;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LCT.Analysis
{
    public class Analyzer
    {
        private string inputStatement;

        

        public Analyzer(string inputStatement)
        {
            this.inputStatement = inputStatement;
        }

        public IParseTree Analyse()
        {
            AntlrInputStream textStream = new AntlrInputStream(this.inputStatement);

            LCTGrammarLexer lexer = new LCTGrammarLexer(textStream);
            CommonTokenStream tokenStream = new CommonTokenStream(lexer);
            LCTGrammarParser parser = new LCTGrammarParser(tokenStream);
            return parser.statement();
        }
    }
}
