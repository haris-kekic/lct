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
        public List<LCTList> Lists { get; private set; }

        protected override string Output(Statement statement)
        {
            string standardtOutput = base.Output(statement);

            this.Lists = new List<LCTList>();

            foreach(var list in base.InMemoryDefinedLists)
            {
                this.Lists.Add(list);
            }

            return standardtOutput;
        }
    }

    public class TestExecEnvironment2 : ExecEnvironment
    {
        public List<LCTList> Lists { get; private set; }

        protected override string Output(Statement statement)
        {
            string standardtOutput = base.Output(statement);

            this.Lists = new List<LCTList>();

            foreach (var list in statement.ListDefinitions)
            {
                this.Lists.Add(list);
            }

            return standardtOutput;
        }
    }

    public class TestExecEnvironment3 : ExecEnvironment
    {
        public Statement Statement { get; private set; }

        protected override string Output(Statement statement)
        {
            this.Statement = statement;
            return base.Output(statement);
        }
    }
}
