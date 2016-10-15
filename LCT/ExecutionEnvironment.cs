using Antlr4.Runtime.Tree;
using LCT.Analysis;
using LCT.Generation;
using LCT.Generation.Preparation;
using LCT.Generation.Preparation.Intermediate;
using LCT.Library;
using LCT.Library.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LCT
{
    /// <summary>
    /// Used to uphold things in memory during a session.
    /// TODO: Add Error Handling
    /// </summary>
    abstract public class ExecEnvironment
    {
        public ExecEnvironment()
        {
            InMemoryDefinedLists = new LctUniqueList();
        }

        protected LctUniqueList InMemoryDefinedLists { get; set; }

        public string Execute(string inputStatement)
        {
            string outputText = string.Empty;

            Analyzer analyser = new Analyzer(inputStatement);
            IParseTree parseTree = analyser.Analyse();
            Generator generator = new Generator(parseTree);
            Statement statement = generator.Evaluate();

            return this.Output(statement);
        }

        /// <summary>
        /// This method can be overridden to handle the translation and output for target environment in a custom way
        /// </summary>
        /// <param name="statement">Parsed and prepared Statement object</param>
        /// <returns></returns>
        virtual protected string Output(Statement statement)
        {
            string outputText = string.Empty;

            if (statement.ListDefinitions != null)
            {
                foreach (var list in statement.ListDefinitions)
                {
                    this.InMemoryDefinedLists.AddOrReplace(list);
                }
            }
            else if (statement.ListsShow != null)
            {
                outputText = OutputDefinedLists();
            }
            else if (statement.ListComprehension != null)
            {
                if (statement.ListComprehension.ArithmeticExpresssionContext != null && statement.ListComprehension.ListDefinitions != null)
                {
                    List<object> results = new List<object>();

                    this.ResolveListReferences(statement.ListComprehension.ListDefinitions);

                    List<Dictionary<string, object>> combinations = statement.ListComprehension.ListDefinitions.GenerateListElementCombinations();
                    foreach (var combination in combinations)
                    {
                        Dictionary<string, decimal> decimalCombination = combination.ToDecimalDictionary();
                        results.Add(new ArithmeticCalculationVisitor(decimalCombination).Visit(statement.ListComprehension.ArithmeticExpresssionContext));
                    }

                    outputText = this.OutputList(results);
                }
            }

            return outputText;
        }

        private void ResolveListReferences(LctUniqueList comprehensionLists)
        {
            /// Iterate through all defined lists in comprehension that have a reference to a list defined before in memory
            /// then refer the elements of the in memory object to the comprehension defined list
            comprehensionLists.Where(cl => !string.IsNullOrEmpty(cl.Reference)).ToList()
                .ForEach(cl =>
                    cl.Elements = (this.InMemoryDefinedLists.FirstOrDefault(dl => dl.Name.Equals(cl.Reference)) != null ? this.InMemoryDefinedLists.FirstOrDefault(dl => dl.Name.Equals(cl.Reference)).Elements : null));

        }

        /// <summary>
        /// String representation can be overridden
        /// </summary>
        /// <param name="parameter"></param>
        /// <returns></returns>
        virtual protected string OutputDefinedLists(params string[] parameter)
        {
            StringBuilder outputBuilder = new StringBuilder();

            foreach (var list in this.InMemoryDefinedLists)
            {
                outputBuilder.Append(list.Name);
                outputBuilder.Append(" = [");
                outputBuilder.Append(string.Join(",", list.Elements));
                outputBuilder.Append("]");
                outputBuilder.Append(Environment.NewLine);
            }

            return outputBuilder.ToString();
        }

        virtual protected string OutputList(IEnumerable<object> list)
        {
            StringBuilder outputBuilder = new StringBuilder();

            outputBuilder.Append("[");
            outputBuilder.Append(string.Join(",", list));
            outputBuilder.Append("]");
            outputBuilder.Append(Environment.NewLine);

            return outputBuilder.ToString();
        }
    }

    public class ConsoleExecEnvironment : ExecEnvironment
    {
        protected override string Output(Statement statement)
        {
            string standardOutput = base.Output(statement);
            Console.Write(standardOutput);
            return standardOutput;
        }
    }
}


