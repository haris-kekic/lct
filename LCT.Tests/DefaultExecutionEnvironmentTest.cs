using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;

namespace LCT.Tests
{
    /// <summary>
    /// Zusammenfassungsbeschreibung für DefaultExecutionEnvironmentTest
    /// </summary>
    [TestClass]
    public class DefaultExecutionEnvironmentTest
    {
        [TestMethod]
        public void ExecEnvironment_Output_InMemoryDefinedList_Count_Test()
        {
            string inputStatement = "def first <- [55,33,44,1,2,66,9,87], second <- [0,1,2,9,8]";

            Application.ExecutionEnvironment<string> execEnvironment = new Application.DefaultExecutionEnvironment();
            Application.ExecutionContext<string> context = new Application.ExecutionContext<string>(inputStatement);
            execEnvironment.Execute(context);

            Assert.AreEqual(2, execEnvironment.AppMemory.DefinedLists.Count);
        }

        [TestMethod]
        public void ExecEnvironment_Output_InMemoryDefinedList_ElementsCount_Test()
        {
            string inputStatement = "def first <- [55,33,44,1,2,66,9,87], second <- [0,1,2,9,8]";

            Application.ExecutionEnvironment<string> execEnvironment = new Application.DefaultExecutionEnvironment();
            Application.ExecutionContext<string> context = new Application.ExecutionContext<string>(inputStatement);
            execEnvironment.Execute(context);

            Assert.AreEqual("first", execEnvironment.AppMemory.DefinedLists.ElementAt(0).Name);
            Assert.AreEqual(8, execEnvironment.AppMemory.DefinedLists.ElementAt(0).Elements.Count);
            CollectionAssert.Contains(execEnvironment.AppMemory.DefinedLists.ElementAt(0).Elements, 55m);

            Assert.AreEqual("second", execEnvironment.AppMemory.DefinedLists.ElementAt(1).Name);
            Assert.AreEqual(5, execEnvironment.AppMemory.DefinedLists.ElementAt(1).Elements.Count);
            CollectionAssert.Contains(execEnvironment.AppMemory.DefinedLists.ElementAt(1).Elements, 9m);
        }

        [TestMethod]
        public void ExecEnvironment_InMemory_DefinedList_Overwrite_Count_Test()
        {
            string inputStatement = "def first <- [55,33,44,1,2,66,9,87], first <- [0,1,2,9,8]";

            Application.ExecutionEnvironment<string> execEnvironment = new Application.DefaultExecutionEnvironment();
            Application.ExecutionContext<string> context = new Application.ExecutionContext<string>(inputStatement);
            execEnvironment.Execute(context);

            Assert.AreEqual(1, execEnvironment.AppMemory.DefinedLists.Count);
        }

        [TestMethod]
        public void ExecEnvironment_InMemory_DefinedList_Overwrite_Element_Test()
        {
            string inputStatement = "def first <- [55,33,44,1,2,66,9,87], first <- [0,1,2,9,8]";

            Application.ExecutionEnvironment<string> execEnvironment = new Application.DefaultExecutionEnvironment();
            Application.ExecutionContext<string> context = new Application.ExecutionContext<string>(inputStatement);
            execEnvironment.Execute(context);

            Assert.AreEqual("first", execEnvironment.AppMemory.DefinedLists.ElementAt(0).Name);
            Assert.AreEqual(5, execEnvironment.AppMemory.DefinedLists.ElementAt(0).Elements.Count);
            CollectionAssert.Contains(execEnvironment.AppMemory.DefinedLists.ElementAt(0).Elements, 8m);
        }

        [TestMethod]
        public void ExecEnvironment_InMemory_DefinedList_Resolving_ReferencedList_Test()
        {
            string inputStatement = "def first <- [55,33,44,1,2,66,9,87]";

            Application.ExecutionEnvironment<string> execEnvironment = new Application.DefaultExecutionEnvironment();
            Application.ExecutionContext<string> context = new Application.ExecutionContext<string>(inputStatement);
            execEnvironment.Execute(context);

            inputStatement = "def second <- first";

            context = new Application.ExecutionContext<string>(inputStatement);
            execEnvironment.Execute(context);

            Assert.AreEqual("second", execEnvironment.AppMemory.DefinedLists.ElementAt(1).Name);
            //Test if they are referencing the same array
            Assert.AreSame(execEnvironment.AppMemory.DefinedLists.ElementAt(0).Elements, execEnvironment.AppMemory.DefinedLists.ElementAt(1).Elements);
        }
    }
}
