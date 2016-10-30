using Microsoft.VisualStudio.TestTools.UnitTesting;
using LCT.Library;
using System.Collections.Generic;
using LCT.Generation.Structure;
using System.Linq;
using System;

namespace LCT.Tests
{
    [TestClass]
    public class Lists_Test
    {
        [TestMethod]
        public void LisDefinition_Combination_Test()
        {
            LctUniqueList listDeifintions = new LctUniqueList();

            listDeifintions.AddOrReplace(new LCTList() { Name = "a", Elements = new List<object>() { 1, 2, 3 } });
            listDeifintions.AddOrReplace(new LCTList() { Name = "b", Elements = new List<object>() { 4, 5, 6 } });
            listDeifintions.AddOrReplace(new LCTList() { Name = "c", Elements = new List<object>() { 7, 8, 9 } });

            ListComprehension listComprehension = new ListComprehension();
            listComprehension.ListDefinitions = listDeifintions;

            listComprehension.GenerateOutputListsOnConditions();

            List<Dictionary<string, object>> combinations = listComprehension.GenerateListElementCombinations();

            Dictionary<string, object> possibleCombination1 = new Dictionary<string, object>();
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

        [TestMethod]
        public void LisDefinition_WithCondition_Test()
        {
            LctUniqueList listDeifintions = new LctUniqueList();

            listDeifintions.AddOrReplace(new LCTList() { Name = "a", Elements = new List<object>() { 1, 2, 3 } });
            listDeifintions.AddOrReplace(new LCTList() { Name = "b", Elements = new List<object>() { 4, 5, 6 } });
            listDeifintions.AddOrReplace(new LCTList() { Name = "c", Elements = new List<object>() { 7, 8, 9 } });

            ListComprehension listComprehension = new ListComprehension();
            listComprehension.ListDefinitions = listDeifintions;
            listComprehension.LogicOperations = new List<LogicOperation>() 
            { 
                new LogicOperation() { ListName = "a", OperationType = LogicOperation.OperationTypeEnum.LowerThenEqual, Value = 2m },
                new LogicOperation() { ListName = "b", OperationType = LogicOperation.OperationTypeEnum.Equal, Value = 6m },
                new LogicOperation() { ListName = "c", OperationType = LogicOperation.OperationTypeEnum.GreaterThen, Value = 7m },
            };

            listComprehension.GenerateOutputListsOnConditions();

            //List "a"
            Assert.IsTrue(listComprehension.OutputLists.ElementAt(0).Elements.Any(e => Convert.ToDecimal(e) == 1));
            Assert.IsFalse(listComprehension.OutputLists.ElementAt(0).Elements.Any(e => Convert.ToDecimal(e) == 3));
            //List "b"
            Assert.IsTrue(listComprehension.OutputLists.ElementAt(1).Elements.Any(e => Convert.ToDecimal(e) == 6));
            Assert.IsFalse(listComprehension.OutputLists.ElementAt(1).Elements.Any(e => Convert.ToDecimal(e) == 5));
            Assert.IsFalse(listComprehension.OutputLists.ElementAt(1).Elements.Any(e => Convert.ToDecimal(e) == 4));
            //List "c"
            Assert.IsTrue(listComprehension.OutputLists.ElementAt(2).Elements.Any(e => Convert.ToDecimal(e) == 9));
            Assert.IsTrue(listComprehension.OutputLists.ElementAt(2).Elements.Any(e => Convert.ToDecimal(e) == 8));
            Assert.IsFalse(listComprehension.OutputLists.ElementAt(2).Elements.Any(e => Convert.ToDecimal(e) == 7));
        }

        [TestMethod]
        public void LisDefinition_Combination_WithCondition_Test()
        {
            LctUniqueList listDeifintions = new LctUniqueList();

            listDeifintions.AddOrReplace(new LCTList() { Name = "a", Elements = new List<object>() { 1, 2, 3 } });
            listDeifintions.AddOrReplace(new LCTList() { Name = "b", Elements = new List<object>() { 4, 5, 6 } });
            listDeifintions.AddOrReplace(new LCTList() { Name = "c", Elements = new List<object>() { 7, 8, 9 } });

            ListComprehension listComprehension = new ListComprehension();
            listComprehension.ListDefinitions = listDeifintions;
            listComprehension.LogicOperations = new List<LogicOperation>() 
            { 
                new LogicOperation() { ListName = "a", OperationType = LogicOperation.OperationTypeEnum.LowerThenEqual, Value = 2m },
                new LogicOperation() { ListName = "b", OperationType = LogicOperation.OperationTypeEnum.Equal, Value = 6m },
                new LogicOperation() { ListName = "c", OperationType = LogicOperation.OperationTypeEnum.GreaterThen, Value = 7m },
            };

            listComprehension.GenerateOutputListsOnConditions();

            List<Dictionary<string, object>> combinations = listComprehension.GenerateListElementCombinations();


            Dictionary<string, object> possibleCombination1 = new Dictionary<string, object>();
            possibleCombination1.Add("a", 1);
            possibleCombination1.Add("b", 4);
            possibleCombination1.Add("c", 7);

            Dictionary<string, object> possibleCombination2 = new Dictionary<string, object>();
            possibleCombination2.Add("a", 3);
            possibleCombination2.Add("b", 5);
            possibleCombination2.Add("c", 8);

            Dictionary<string, object> possibleCombination3 = new Dictionary<string, object>();
            possibleCombination3.Add("a", 1);
            possibleCombination3.Add("b", 6);
            possibleCombination3.Add("c", 8);

            Dictionary<string, object> possibleCombination4 = new Dictionary<string, object>();
            possibleCombination4.Add("a", 2);
            possibleCombination4.Add("b", 6);
            possibleCombination4.Add("c", 9);


            Assert.AreEqual(4, combinations.Count);
            //Old combinations without condition
            Assert.IsFalse(combinations.Contains(possibleCombination1, new DictionaryCombinationsComparer()));
            Assert.IsFalse(combinations.Contains(possibleCombination2, new DictionaryCombinationsComparer()));
            //New combinations with conditions
            Assert.IsTrue(combinations.Contains(possibleCombination3, new DictionaryCombinationsComparer()));
            Assert.IsTrue(combinations.Contains(possibleCombination4, new DictionaryCombinationsComparer()));
        }
    }
}
