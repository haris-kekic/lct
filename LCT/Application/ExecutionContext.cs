using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LCT.Application
{
    public class ExecutionContext<OutputType>
    {
        public string Input { get; set; }
        public OutputType Output { get; set; }
        public ExecutionContext(string inputStatement)
        {
            this.Input = inputStatement;
        }
    }
}
