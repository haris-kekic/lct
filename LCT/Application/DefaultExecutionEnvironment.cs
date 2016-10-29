using Antlr4.Runtime.Tree;
using LCT.Analysis;
using LCT.Generation;
using LCT.Generation.Structure;
using LCT.Translation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LCT.Application
{
    public class DefaultExecutionEnvironment : ExecutionEnvironment<string>
    {
        public override void Execute(ExecutionContext<string> context)
        {
            Analyzer analyser = new Analyzer(context.Input);
            IParseTree parseTree = analyser.Analyse();

            IGenerator generator = new DefaultGenerator();
            Statement statement = (Statement)generator.Generate(parseTree);
            
            InterpretationContext<string> interpretationContext = new InterpretationContext<string>(this, statement);
            IInterpreter<string> interpreter = new DefaultInterpreter();
            interpreter.Interpret(interpretationContext);

            context.Output = interpretationContext.Output;

            
        }
    }
}
