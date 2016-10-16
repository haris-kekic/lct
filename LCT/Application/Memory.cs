using LCT.Library;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LCT
{
    public class Memory
    {
        public Memory()
        {
            this.DefinedLists = new LctUniqueList();
        }

        public LctUniqueList DefinedLists { get; set; }
    }
}
