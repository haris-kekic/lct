using LCT.Generation.Preparation.Intermediate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LCT.Tests.Helper
{
    public class TestExecEnvironment : ExecEnvironment
    {
        public string OutputText { get; private set; }

        public List<LCTList> Lists { get; private set; }

        protected override void Output(string outputText)
        {
            this.OutputText = outputText;

            this.Lists = new List<LCTList>();

            foreach(var list in base.DefinedLists)
            {
                this.Lists.Add(list);
            }
        }
    }
}
