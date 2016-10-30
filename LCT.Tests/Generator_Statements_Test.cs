using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using LCT.Analysis;
using Antlr4.Runtime.Tree;
using LCT.Generation;
using LCT.Generation.Structure;
using System.Linq;

namespace LCT.Tests
{
    [TestClass]
    public class Generator_Statements_Test
    {
        /// <summary>
        /// Helper method
        /// </summary>
        /// <param name="inputText"></param>
        /// <returns></returns>
        private Statement GenerateStatement(string inputText)
        {
            Analyzer analyser = new Analyzer(inputText);
            IParseTree parseTree = analyser.Analyse();
            IGenerator generator = new DefaultGenerator();
            Statement statement = (Statement)generator.Generate(parseTree);
            return statement;
        }

        [TestMethod]
        public void Statement_DefiningLists_IsNotNull_Test()
        {
            string inputText = "def x <- [1,2,4], y <- [0,1,2,9,8]";
            Statement statement = this.GenerateStatement(inputText);

            Assert.IsNotNull(statement.ListDefinitions);
        }

        [TestMethod]
        public void Statement_DefiningLists_Statement_ListComprehension_IsNull_Test()
        {
            string inputText = "def x <- [1,2,4], y <- [0,1,2,9,8]";

            Statement statement = this.GenerateStatement(inputText);

            Assert.IsNull(statement.ListComprehension);
        }

        [TestMethod]
        public void Statement_DefiningLists_Statement_ListShow_IsNull_Test()
        {
            string inputText = "def x <- [1,2,4], y <- [0,1,2,9,8]";
            Statement statement = this.GenerateStatement(inputText);

            Assert.IsNull(statement.ListsShow);
        }

        [TestMethod]
        public void Statement_DefiningLists_Count_Test()
        {
            string inputText = "def x <- [1,2,4], y <- [0,1,2,9,8]";
            Statement statement = this.GenerateStatement(inputText);

            Assert.AreEqual(2, statement.ListDefinitions.Count);
        }

        [TestMethod]
        public void Statement_DefiningLists_CorrectElement_Test()
        {
            string inputText = "def x <- [1,2,4], y <- [0,1,2,9,8]";

            Statement statement = this.GenerateStatement(inputText);

            CollectionAssert.Contains(statement.ListDefinitions.ElementAt(0).Elements, 4m);
            CollectionAssert.Contains(statement.ListDefinitions.ElementAt(1).Elements, 8m);
        }

        [TestMethod]
        public void Statement_DefineLists_EmptyList_Test()
        {
            string inputText = "def emptyList <- []";

            Statement statement = this.GenerateStatement(inputText);

            Assert.AreEqual(0, statement.ListDefinitions.ElementAt(0).Elements.Count);
        }


        [TestMethod]
        public void Statement_DefineLists_AutListsLimited_Test()
        {
            string inputText = "def autoList <- [1..10]";
            Statement statement = this.GenerateStatement(inputText);

            Assert.AreEqual(10, statement.ListDefinitions.ElementAt(0).Elements.Count);
            Assert.AreEqual(6m, statement.ListDefinitions.ElementAt(0).Elements[5]);
        }

        [TestMethod]
        public void Statement_DefineLists_AutListsLeftUnlimted_Test()
        {
            string inputText = "def autoList <- [..10]";
            Statement statement = this.GenerateStatement(inputText);

            Assert.AreEqual(Convert.ToDecimal(Int16.MinValue), statement.ListDefinitions.ElementAt(0).Elements.FirstOrDefault());
        }

        [TestMethod]
        public void Statement_ListsShow_IsNotNull_Test()
        {
            string inputText = "show";
            Statement statement = this.GenerateStatement(inputText);

            Assert.IsNotNull(statement.ListsShow);
        }

        [TestMethod]
        public void Statement_ListsShow_Statement_ListComprehension_IsNull_Test()
        {
            string inputText = "show";
            Statement statement = this.GenerateStatement(inputText);

            Assert.IsNull(statement.ListComprehension);

        }

        [TestMethod]
        public void Statement_ListsShow_Statement_ListDefinitions_IsNull_Test()
        {
            string inputText = "show";
            Statement statement = this.GenerateStatement(inputText);

            Assert.IsNull(statement.ListDefinitions);
        }

        [TestMethod]
        public void Statement_ListComprehension_IsNotNull_Test()
        {
            string inputStatement = "[x | x <- [1,2,4,3], y <- [0,1,2,9,7,6]]";

            Statement statement = this.GenerateStatement(inputStatement);

            Assert.IsNotNull(statement.ListComprehension);
        }

        [TestMethod]
        public void Statement_ListComprehension_Statement_ListDefinitions_IsNull_Test()
        {
            string inputText = "[x | x <- [1,2,4,3], y <- [0,1,2,9,7,6]]";

            Statement statement = this.GenerateStatement(inputText);

            Assert.IsNull(statement.ListDefinitions);
        }

        [TestMethod]
        public void Statement_ListComprehension_Statement_ListShow_IsNull_Test()
        {
            string inputText = "[x | x <- [1,2,4,3], y <- [0,1,2,9,7,6]]";
            Statement statement = this.GenerateStatement(inputText);

            Assert.IsNull(statement.ListsShow);
        }

        [TestMethod]
        public void Statement_ListComprehension_ListsDefined_Count_Test()
        {
            string inputStatement = "[x | x <- [1,2,5,6], y <- [0,1,2,9,3], z <- []]";

            Statement statement = this.GenerateStatement(inputStatement);

            Assert.AreEqual(3, statement.ListComprehension.ListDefinitions.Count);
        }

        [TestMethod]
        public void Statement_ListComprehension_ListsDefined_Elements_Test()
        {
            string inputStatement = "[x | x <- [1,2,5,6], y <- [0,1,2,9,3], z <- []]";

            Statement statement = this.GenerateStatement(inputStatement);

            Assert.AreEqual("y", statement.ListComprehension.ListDefinitions.ElementAt(1).Name);
            Assert.AreEqual(9m, statement.ListComprehension.ListDefinitions.ElementAt(1).Elements[3]);
        }

        [TestMethod]
        public void Statement_ListComprehension_ArithExpressionContext_IsNotNull_Test()
        {
            string inputStatement = "[x | x <- [1,2,4,3], y <- [0,1,2,9,7,6]]";

            Statement statement = this.GenerateStatement(inputStatement);

            Assert.IsNotNull(statement.ListComprehension.ArithmeticExpresssionContext);
        }

        [TestMethod]
        public void Statement_ListComprehension_LogicOperation_IsNotNull_Test()
        {
            string inputStatement = "[x | x <- [1,2,4,3], y <- [0,1,2,9,7,6], x < 5]";

            Statement statement = this.GenerateStatement(inputStatement);

            Assert.IsNotNull(statement.ListComprehension.LogicOperations);
        }

        [TestMethod]
        public void Statement_ListComprehension_LogicOperation_Single_Content_Test()
        {
            string inputStatement = "[x | x <- [1,2,4,3], y <- [0,1,2,9,7,6], x < 5]";

            Statement statement = this.GenerateStatement(inputStatement);

            Assert.AreEqual("x", statement.ListComprehension.LogicOperations[0].ListName);
            Assert.AreEqual(LogicOperation.OperationTypeEnum.LowerThen, statement.ListComprehension.LogicOperations[0].OperationType);
            Assert.AreEqual(5, statement.ListComprehension.LogicOperations[0].Value);
        }

        [TestMethod]
        public void Statement_ListComprehension_LogicOperation_Multiple_Content_Test()
        {
            string inputStatement = "[x | x <- [1,2,4,3], y <- [0,1,2,9,7,6], x < 5, y <= 10]";

            Statement statement = this.GenerateStatement(inputStatement);

            Assert.AreEqual("y", statement.ListComprehension.LogicOperations[1].ListName);
            Assert.AreEqual(LogicOperation.OperationTypeEnum.LowerThenEqual, statement.ListComprehension.LogicOperations[1].OperationType);
            Assert.AreEqual(10, statement.ListComprehension.LogicOperations[1].Value);
        }

        [TestMethod]
        public void Statement_ListComprehension_LogicOperation_OperationType_Equal_Test()
        {
            string inputStatement = "[x | x <- [1,2,4,3], y <- [0,1,2,9,7,6], x = 5]";

            Statement statement = this.GenerateStatement(inputStatement);

            Assert.AreEqual(LogicOperation.OperationTypeEnum.Equal, statement.ListComprehension.LogicOperations[0].OperationType);
        }

        [TestMethod]
        public void Statement_ListComprehension_LogicOperation_OperationType_LowerThen_Test()
        {
            string inputStatement = "[x | x <- [1,2,4,3], y <- [0,1,2,9,7,6], x < 5]";

            Statement statement = this.GenerateStatement(inputStatement);

            Assert.AreEqual(LogicOperation.OperationTypeEnum.LowerThen, statement.ListComprehension.LogicOperations[0].OperationType);
        }

        [TestMethod]
        public void Statement_ListComprehension_LogicOperation_OperationType_LowerThenEqual_Test()
        {
            string inputStatement = "[x | x <- [1,2,4,3], y <- [0,1,2,9,7,6], x <= 5]";

            Statement statement = this.GenerateStatement(inputStatement);

            Assert.AreEqual(LogicOperation.OperationTypeEnum.LowerThenEqual, statement.ListComprehension.LogicOperations[0].OperationType);
        }

        [TestMethod]
        public void Statement_ListComprehension_LogicOperation_OperationType_GreaterThen_Test()
        {
            string inputStatement = "[x | x <- [1,2,4,3], y <- [0,1,2,9,7,6], x > 5]";

            Statement statement = this.GenerateStatement(inputStatement);

            Assert.AreEqual(LogicOperation.OperationTypeEnum.GreaterThen, statement.ListComprehension.LogicOperations[0].OperationType);
        }

        [TestMethod]
        public void Statement_ListComprehension_LogicOperation_OperationType_GreaterThenEqual_Test()
        {
            string inputStatement = "[x | x <- [1,2,4,3], y <- [0,1,2,9,7,6], x >= 5]";

            Statement statement = this.GenerateStatement(inputStatement);

            Assert.AreEqual(LogicOperation.OperationTypeEnum.GreaterThenEqual, statement.ListComprehension.LogicOperations[0].OperationType);
        }
    }
}
