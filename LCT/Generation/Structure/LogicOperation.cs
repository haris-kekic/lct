using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LCT.Generation.Structure
{
    public class LogicOperation
    {
        public enum OperationTypeEnum
        {
            Undefined,
            Equal,
            GreaterThen,
            GreaterThenEqual,
            LowerThen,
            LowerThenEqual
        }

        public OperationTypeEnum OperationType { get; set; }
        public string ListName { get; set; }
        public decimal Value { get; set; }
    }
}
