using Microsoft.VisualStudio.TestTools.UnitTesting;
using LCT.Library;
using System.Collections.Generic;
using LCT.Generation.Structure;
using System.Linq;

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

            List<Dictionary<string, object>> combinations = listDeifintions.GenerateListElementCombinations();

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
    }
}
