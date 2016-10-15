using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using LCT.Analysis;
using LCT.Generation;
using LCT.Generation.Preparation;
using System.Linq;
using LCT.Generation.Preparation.Intermediate;
using LCT.Tests.Helper;
using LCT.Library;
using System.Collections.Generic;

namespace LCT.Tests
{
    [TestClass]
    public class BasicTests
    {
        [TestMethod]
        public void Statement_DefineLists_Test()
        {
            TestExecEnvironment2 execEnvironment = new TestExecEnvironment2();

            string inputStatement = "def x <- [1,2,4], y <- [0,1,2,9,8]";
            string outputText = execEnvironment.Execute(inputStatement);

            Assert.AreEqual(2, execEnvironment.Lists.Count);

            Assert.AreEqual("x", execEnvironment.Lists.ElementAt(0).Name);
            Assert.AreEqual(3, execEnvironment.Lists.ElementAt(0).Elements.Count);
            CollectionAssert.Contains(execEnvironment.Lists.ElementAt(0).Elements, 1m);
            CollectionAssert.Contains(execEnvironment.Lists.ElementAt(0).Elements, 4m);

            Assert.AreEqual("y", execEnvironment.Lists.ElementAt(1).Name);
            Assert.AreEqual(5, execEnvironment.Lists.ElementAt(1).Elements.Count);
            CollectionAssert.Contains(execEnvironment.Lists.ElementAt(1).Elements, 0m);
            CollectionAssert.Contains(execEnvironment.Lists.ElementAt(1).Elements, 8m);
        }

        [TestMethod]
        public void Statement_DefineLists_EmptyList_Test()
        {
            TestExecEnvironment2 execEnvironment = new TestExecEnvironment2();

            string inputStatement = "def emptyList <- []";
            string outputText = execEnvironment.Execute(inputStatement);

            Assert.AreEqual(0, execEnvironment.Lists.ElementAt(0).Elements.Count);
        }

        [TestMethod]
        public void Statement_DefineLists_AutListsLimited_Test()
        {
            TestExecEnvironment2 execEnvironment = new TestExecEnvironment2();

            string inputStatement = "def autoList <- [1..10]";
            string outputText = execEnvironment.Execute(inputStatement);

            Assert.AreEqual(10, execEnvironment.Lists.ElementAt(0).Elements.Count);
            Assert.AreEqual(6m, execEnvironment.Lists.ElementAt(0).Elements[5]);
        }

        [TestMethod]
        public void Statement_DefineLists_AutListsLeftUnlimted_Test()
        {
            TestExecEnvironment2 execEnvironment = new TestExecEnvironment2();

            string inputStatement = "def autoList <- [..10]";
            string outputText = execEnvironment.Execute(inputStatement);

            Assert.AreEqual(Convert.ToDecimal(Int16.MinValue), execEnvironment.Lists.ElementAt(0).Elements.FirstOrDefault());
        }

        [TestMethod]
        public void ExecEnvironment_InMemoryDefinedList_Test()
        {
            TestExecEnvironment execEnvironment = new TestExecEnvironment();

            string inputStatement = "def first <- [55,33,44,1,2,66,9,87], second <- [0,1,2,9,8]";
            string outputText = execEnvironment.Execute(inputStatement);

            Assert.AreEqual(2, execEnvironment.Lists.Count);

            Assert.AreEqual("first", execEnvironment.Lists.ElementAt(0).Name);
            Assert.AreEqual(8, execEnvironment.Lists.ElementAt(0).Elements.Count);
            CollectionAssert.Contains(execEnvironment.Lists.ElementAt(0).Elements, 33m);
            CollectionAssert.Contains(execEnvironment.Lists.ElementAt(0).Elements, 87m);

            Assert.AreEqual("second", execEnvironment.Lists.ElementAt(1).Name);
            Assert.AreEqual(5, execEnvironment.Lists.ElementAt(1).Elements.Count);
            CollectionAssert.Contains(execEnvironment.Lists.ElementAt(1).Elements, 0m);
            CollectionAssert.Contains(execEnvironment.Lists.ElementAt(1).Elements, 8m);
        }

        [TestMethod]
        public void ExecEnvironment_InMemory_DefinedList_Overwrite_Test()
        {
            TestExecEnvironment execEnvironment = new TestExecEnvironment();

            string inputStatement = "def first <- [55,33,44,1,2,66,9,87], first <- [0,1,2,9,8]";
            execEnvironment.Execute(inputStatement);

            Assert.AreEqual(1, execEnvironment.Lists.Count);

            Assert.AreEqual("first", execEnvironment.Lists.ElementAt(0).Name);
            Assert.AreEqual(5, execEnvironment.Lists.ElementAt(0).Elements.Count);
            CollectionAssert.Contains(execEnvironment.Lists.ElementAt(0).Elements, 0m);
            CollectionAssert.Contains(execEnvironment.Lists.ElementAt(0).Elements, 8m);
        }

        [TestMethod]
        public void Statement_ListComprehension_IsNotNull_Test()
        {
            TestExecEnvironment3 execEnvironment = new TestExecEnvironment3();

            string inputStatement = "[x | x <- [1,2,4,3], y <- [0,1,2,9,7,6]]";
            string outputText = execEnvironment.Execute(inputStatement);

            Assert.IsNotNull(execEnvironment.Statement.ListComprehension);
        }

        [TestMethod]
        public void Statement_ListComprehension_ListsDefined_Test()
        {
            TestExecEnvironment3 execEnvironment = new TestExecEnvironment3();

            string inputStatement = "[x | x <- [1,2,5,6], y <- [0,1,2,9,3], z <- []]";
            string outputText = execEnvironment.Execute(inputStatement);

            Assert.AreEqual(3, execEnvironment.Statement.ListComprehension.ListDefinitions.Count);
            Assert.AreEqual("y" , execEnvironment.Statement.ListComprehension.ListDefinitions.ElementAt(1).Name);
            Assert.AreEqual(9m, execEnvironment.Statement.ListComprehension.ListDefinitions.ElementAt(1).Elements[3]);
        }

        [TestMethod]
        public void Statement_ListComprehension_ArithExpressionContext_IsNotNull_Test()
        {
            TestExecEnvironment3 execEnvironment = new TestExecEnvironment3();

            string inputStatement = "[x | x <- [1,2,4,3], y <- [0,1,2,9,7,6]]";
            string outputText = execEnvironment.Execute(inputStatement);

            Assert.IsNotNull(execEnvironment.Statement.ListComprehension.ArithmeticExpresssionContext);
        }

        [TestMethod]
        public void LisDefinition_Combination_Test()
        {
            LctUniqueList listDeifintions = new LctUniqueList();

            listDeifintions.AddOrReplace(new LCTList() { Name = "a", Elements = new List<object>() { 1, 2, 3}});
            listDeifintions.AddOrReplace(new LCTList() { Name = "b", Elements = new List<object>() { 4, 5, 6}});
            listDeifintions.AddOrReplace(new LCTList() { Name = "c", Elements = new List<object>() { 7, 8, 9 }});

            List<Dictionary<string, object>> combinations = listDeifintions.GenerateListElementCombinations();

            Dictionary<string, object> possibleCombination1 = new Dictionary<string,object>();
            possibleCombination1.Add("a", 1);
            possibleCombination1.Add("b", 4);
            possibleCombination1.Add("c", 7);

            Dictionary<string, object> possibleCombination2 = new Dictionary<string, object>();
            possibleCombination2.Add("a", 3);
            possibleCombination2.Add("b", 5);
            possibleCombination2.Add("c", 8);

            Assert.AreEqual(27, combinations.Count);
            Assert.IsTrue(combinations.Contains(possibleCombination1, new DictionaryCombinationsComparer()));
            Assert.IsTrue(combinations.Contains(possibleCombination2, new DictionaryCombinationsComparer()));
        }
    }
}
