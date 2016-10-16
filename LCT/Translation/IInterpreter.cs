using LCT.Generation.Structure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LCT.Translation
{
    /// <summary>
    /// IInterpreter can be implemented to shape your own translation output
    /// </summary>
   public interface IInterpreter<OutputType>
   {
       void Interpret(InterpretationContext<OutputType> context);
   }
}
