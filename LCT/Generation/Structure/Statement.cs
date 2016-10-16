using LCT.Library;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LCT.Generation.Structure
{
    public class Statement : InterpretationUnit
    {
        public ListsShow ListsShow { get; set; }

        public LctUniqueList ListDefinitions { get; set; }

        public ListComprehension ListComprehension { get; set; }
    }  
}
