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
        private string inputCode;

        public Analyzer(string inputCode)
        {
            this.inputCode = inputCode;
        }

        public IParseTree Analyse()
        {
            AntlrInputStream textStream = new AntlrInputStream(this.inputCode);

            LCTGrammarLexer lexer = new LCTGrammarLexer(textStream);
            CommonTokenStream tokenStream = new CommonTokenStream(lexer);
            LCTGrammarParser parser = new LCTGrammarParser(tokenStream);
            return parser.statement();
        }
    }
}
