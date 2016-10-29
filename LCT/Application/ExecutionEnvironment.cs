using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LCT.Application
{
    public abstract class ExecutionEnvironment<OutputType>
    {
        public ExecutionEnvironment()
        {
            this.AppMemory = new Memory();
        }

        public Memory AppMemory { get; set; }

        public abstract void Execute(ExecutionContext<OutputType> context);
    }
}
