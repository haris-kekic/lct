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
        public InterpretationContext(ExececutionEnvironment<OutputType> environment, InterpretationUnit inputUnit)
        {
            this.Environment = environment;
            this.Input = inputUnit;
        }

        public InterpretationUnit Input { get; protected set; }

        public ExececutionEnvironment<OutputType> Environment { get; set; }

        public OutputType Output { get; set; }
    }
}
