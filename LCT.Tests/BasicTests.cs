using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using LCT.Analysis;
using LCT.Generation;
using LCT.Generation.Preparation;
using System.Linq;
using LCT.Generation.Preparation.Intermediate;

namespace LCT.Tests
{
    [TestClass]
    public class BasicTests
    {
        [TestMethod]
        public void StatementIsListDefinitionTest()
        {
            string inputStatement = "def x <- [1,2,4], y <- [0,1,2,9,8]";
            Analyzer analyser = new Analyzer(inputStatement);
            Generator generator = new Generator(analyser.Analyse());
            Statement statement = generator.Evaluate();

            Assert.AreEqual(2, statement.ListDefinitions.Count);

            Assert.AreEqual("x", statement.ListDefinitions.ElementAt(0).Name);
            Assert.AreEqual(3, statement.ListDefinitions.ElementAt(0).Elements.Count);
            CollectionAssert.Contains(statement.ListDefinitions.ElementAt(0).Elements, 1m);
            CollectionAssert.Contains(statement.ListDefinitions.ElementAt(0).Elements, 4m);

            Assert.AreEqual("y", statement.ListDefinitions.ElementAt(1).Name);
            Assert.AreEqual(5, statement.ListDefinitions.ElementAt(1).Elements.Count);
            CollectionAssert.Contains(statement.ListDefinitions.ElementAt(1).Elements, 0m);
            CollectionAssert.Contains(statement.ListDefinitions.ElementAt(1).Elements, 8m);
        }

        //[TestMethod]
        //public void StatementListDefinitionParseTest()
        //{
        //    //string inputStatement = "set xList <- [1,2,4,6]";
        //    //Analyzer analyser = new Analyzer(inputStatement);
        //    //Generator generator = new Generator(analyser.Analyse());
        //    //Statement statement = generator.Evaluate();
        //    //ListDefinition listDefStatement = (ListDefinition)statement;

        //    //Assert.AreEqual(listDefStatement.Name, "xList");
        //    //CollectionAssert.Contains(listDefStatement.Elements, 1m);
        //    //CollectionAssert.Contains(listDefStatement.Elements, 2m);
        //    //CollectionAssert.Contains(listDefStatement.Elements, 4m);
        //    //CollectionAssert.Contains(listDefStatement.Elements, 6m);
        //}

        //[TestMethod]
        //public void StatementListDefinitionDefinedEqualTest()
        //{
        //    string listName = "xList";
        //    string inputStatement = string.Format("set {0} <- [1,2,4,6]", listName);
        //    Analyzer analyser = new Analyzer(inputStatement);
        //    Generator generator = new Generator(analyser.Analyse());
        //    Statement statement = generator.Evaluate();
        //    ListDefinition listDefStatement = (ListDefinition)statement;

        //    //Makesure it was added
        //    ListDefinition definedList = generator.DefinedLists.FirstOrDefault(dl => dl.Name.Equals(listName, StringComparison.CurrentCultureIgnoreCase));
        //    Assert.IsNotNull(definedList);

        //    Assert.AreEqual(listDefStatement.Name, listName);
        //    CollectionAssert.Contains(definedList.Elements, 1m);
        //    CollectionAssert.Contains(definedList.Elements, 2m);
        //    CollectionAssert.Contains(definedList.Elements, 4m);
        //    CollectionAssert.Contains(definedList.Elements, 6m);

        //    inputStatement = string.Format("set {0} <- [2,0,4]", listName);
        //    analyser = new Analyzer(inputStatement);
        //    generator.ParseTree = analyser.Analyse();
        //    statement = generator.Evaluate();
        //    listDefStatement = (ListDefinition)statement;

        //    //Make sure it was overwritten and not added
        //    Assert.AreEqual(1, generator.DefinedLists.Where(dl => dl.Name.Equals(listName, StringComparison.CurrentCultureIgnoreCase)).Count());

        //    definedList = generator.DefinedLists.FirstOrDefault(dl => dl.Name.Equals(listName, StringComparison.CurrentCultureIgnoreCase));

        //    Assert.AreEqual(listDefStatement.Name, listName);
        //    CollectionAssert.Contains(definedList.Elements, 2m);
        //    CollectionAssert.Contains(definedList.Elements, 0m);
        //    CollectionAssert.Contains(definedList.Elements, 4m);
        //}

        //[TestMethod]
        //public void StatementListDefinitionDefinedAddTest()
        //{
        //    string listName1 = "xList";
            
        //    string inputStatement = string.Format("set {0} <- [1,2,4,6]", listName1);
        //    Analyzer analyser = new Analyzer(inputStatement);
        //    Generator generator = new Generator(analyser.Analyse());
        //    Statement statement = generator.Evaluate();
        //    ListDefinition listDefStatement = (ListDefinition)statement;

        //    //Makesure it was added
        //    ListDefinition definedList = generator.DefinedLists.FirstOrDefault(dl => dl.Name.Equals(listName1, StringComparison.CurrentCultureIgnoreCase));
        //    Assert.IsNotNull(definedList);

        //    Assert.AreEqual(listDefStatement.Name, listName1);
        //    CollectionAssert.Contains(definedList.Elements, 1m);
        //    CollectionAssert.Contains(definedList.Elements, 2m);
        //    CollectionAssert.Contains(definedList.Elements, 4m);
        //    CollectionAssert.Contains(definedList.Elements, 6m);

        //    string listName2 = "xList2";

        //    inputStatement = string.Format("set {0} <- [2,0,4]", listName2);
        //    analyser = new Analyzer(inputStatement);
        //    generator.ParseTree = analyser.Analyse();
        //    statement = generator.Evaluate();
        //    listDefStatement = (ListDefinition)statement;

        //    //Make sure it was added
        //    Assert.AreEqual(2, generator.DefinedLists.Count);

        //    definedList = generator.DefinedLists.FirstOrDefault(dl => dl.Name.Equals(listName2, StringComparison.CurrentCultureIgnoreCase));

        //    Assert.AreEqual(listDefStatement.Name, listName2);
        //    CollectionAssert.Contains(definedList.Elements, 2m);
        //    CollectionAssert.Contains(definedList.Elements, 0m);
        //    CollectionAssert.Contains(definedList.Elements, 4m);
        //}
    }
}
