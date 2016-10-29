using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using LCT.Analysis;
using LCT.Generation.Structure;
using Antlr4.Runtime.Tree;
using LCT.Generation;
using LCT.Translation;

namespace LCT.Tests
{
    [TestClass]
    public class DefaultInterpreter_Test
    {
        /// <summary>
        /// Helper method
        /// </summary>
        /// <param name="inputText"></param>
        /// <returns></returns>
        private InterpretationUnit GenerateStatement(string inputText)
        {
            Analyzer analyser = new Analyzer(inputText);
            IParseTree parseTree = analyser.Analyse();
            IGenerator generator = new DefaultGenerator();
            Statement statement = (Statement)generator.Generate(parseTree);
            return statement;
        }

        [TestMethod]
        public void Statement_ListComprehension_MultipleListsCalculation_Output_Test()
        {
            string inputText = "[x*y | x <- [1,2,3], y <- [1,2,3]]";
            Application.DefaultExecutionEnvironment environment = new Application.DefaultExecutionEnvironment();
            InterpretationContext<string> interpretationContext = new InterpretationContext<string>(environment, this.GenerateStatement(inputText));
            IInterpreter<string> interpreter = new DefaultInterpreter();
            interpreter.Interpret(interpretationContext);

            Assert.AreEqual("[1,2,3,2,4,6,3,6,9]", interpretationContext.Output);
        }

        [TestMethod]
        public void Statement_ListComprehension_SingleList_Output_Test()
        {
            string inputText = "[x | x <- [1,2,3]]";
            Application.DefaultExecutionEnvironment environment = new Application.DefaultExecutionEnvironment();
            InterpretationContext<string> interpretationContext = new InterpretationContext<string>(environment, this.GenerateStatement(inputText));
            IInterpreter<string> interpreter = new DefaultInterpreter();
            interpreter.Interpret(interpretationContext);

            Assert.AreEqual("[1,2,3]", interpretationContext.Output);
        }
    }
}
