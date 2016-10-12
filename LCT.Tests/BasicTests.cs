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
    }
}
