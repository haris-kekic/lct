using LCT.Generation;
using LCT.Generation.Structure;
using LCT.Library;
using LCT.Library.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LCT.Translation
{
    public class DefaultInterpreter : IInterpreter<string>
    {
        public void Interpret(InterpretationContext<string> context)
        {
            if (context.Input == null)
            {
                //TODO: Throw Exeption
            }

            if (context.Environment == null)
            {
                //TODO: Throw Exeption
            }

            if (context.Input is Statement)
            {
                Statement statement = (Statement)context.Input;
                Memory appMemory = context.Environment.AppMemory;

                if (statement.ListDefinitions != null)
                {
                    this.ResolveListReferencesFromInMemoryDefined(appMemory, statement.ListDefinitions);

                    foreach (var list in statement.ListDefinitions)
                    {
                        appMemory.DefinedLists.Add(list);
                    } 
                }
                else if (statement.ListsShow != null)
                {
                    context.Output = OutputInMemoryDefinedLists(appMemory);
                }
                else if (statement.ListComprehension != null)
                {
                    if (statement.ListComprehension.ArithmeticExpresssionContext != null && statement.ListComprehension.ListDefinitions != null)
                    {
                        List<object> results = new List<object>();

                        this.ResolveListReferencesFromInMemoryDefined(appMemory, statement.ListComprehension.ListDefinitions);

                        //TODO: Apply logic operations and conditions

                        List<Dictionary<string, object>> combinations = statement.ListComprehension.ListDefinitions.GenerateListElementCombinations();
                        foreach (var combination in combinations)
                        {
                            Dictionary<string, decimal> decimalCombination = combination.ToDecimalDictionary();
                            results.Add(new ArithmeticCalculationVisitor(decimalCombination).Visit(statement.ListComprehension.ArithmeticExpresssionContext));
                        }

                        context.Output = this.OutputListElements(results);
                    }
                }

            }
        }

        /// <summary>
        /// String representation can be overridden
        /// </summary>
        /// <param name="parameter"></param>
        /// <returns></returns>
        protected string OutputInMemoryDefinedLists(Memory appMemory)
        {
            StringBuilder outputBuilder = new StringBuilder();

            foreach (var list in appMemory.DefinedLists)
            {
                outputBuilder.Append(list.Name);
                outputBuilder.Append(" = ");
                outputBuilder.Append(this.OutputListElements(list.Elements));
                outputBuilder.Append(Environment.NewLine);
            }

            return outputBuilder.ToString();
        }

         protected string OutputListElements(IEnumerable<object> elements)
        {
            StringBuilder outputBuilder = new StringBuilder();

            outputBuilder.Append("[");
            outputBuilder.Append(string.Join(",", elements));
            outputBuilder.Append("]");

            return outputBuilder.ToString();
        }

        private void ResolveListReferencesFromInMemoryDefined(Memory appMemory, LctUniqueList newDefinedList)
        {
            /// Iterate through all defined lists in comprehension that have a reference to a list defined before in memory
            /// then refer the elements of the in memory object to the comprehension defined list
            newDefinedList.Where(cl => !string.IsNullOrEmpty(cl.Reference)).ToList()
                .ForEach(cl =>
                    cl.Elements = (appMemory.DefinedLists.FirstOrDefault(dl => dl.Name.Equals(cl.Reference)) != null ? appMemory.DefinedLists.FirstOrDefault(dl => dl.Name.Equals(cl.Reference)).Elements : null));

        }
    }
}
