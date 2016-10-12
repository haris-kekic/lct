using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LCT.Generation.Preparation.Intermediate
{
    /// <summary>
    /// To avoid naming conflicts with System.Collections.Generic.List
    /// </summary>
    public class LCTList : IntermediateCode
    {
        public LCTList()
        {
            this.Elements = new List<object>();
        }

        public string Name { get; set; }

        public List<object> Elements { get; set; }
    }
}
