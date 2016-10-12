using LCT.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LCT.Generation.Preparation.Intermediate
{
    public class Statement : IntermediateCode
    {
        public ListsShow ListsShow { get; set; }

        public LctUniqueList ListDefinitions { get; set; }

        public ListComprehension Comprehension { get; set; }
    }  
}
