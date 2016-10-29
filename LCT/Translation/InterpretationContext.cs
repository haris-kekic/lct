using LCT.Application;
using LCT.Generation.Structure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LCT.Translation
{
    public class InterpretationContext<OutputType>
    {
        public InterpretationContext(ExecutionEnvironment<OutputType> environment, InterpretationUnit inputUnit)
        {
            this.Environment = environment;
            this.Input = inputUnit;
        }

        public InterpretationUnit Input { get; protected set; }

        public ExecutionEnvironment<OutputType> Environment { get; set; }

        public OutputType Output { get; set; }
    }
}
