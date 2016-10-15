using LCT.Generation.Preparation.Intermediate;
using System;
using System.Collections.Generic;
using System.Linq;

namespace LCT.Library
{
    public class LctListEqualityComparer : IEqualityComparer<LCTList>
    {
        public bool Equals(LCTList x, LCTList y)
        {
            return x.Name.Equals(y.Name, StringComparison.CurrentCultureIgnoreCase);
        }

        public int GetHashCode(LCTList obj)
        {
            return obj.Name.GetHashCode();
        }
    }

    //Help override LctLists with the same name
    public class LctUniqueList : HashSet<LCTList>
    {
        public LctUniqueList()
            : base(new LctListEqualityComparer())
        {
        }

        public void AddOrReplace(LCTList listDefinition)
        {
            if (!this.Add(listDefinition))
            {
                LCTList existingList = this.FirstOrDefault(x => x.Name.Equals(listDefinition.Name, StringComparison.CurrentCultureIgnoreCase));

                if (existingList != null)
                {
                    existingList.Elements.Clear();
                    existingList.Elements.AddRange(listDefinition.Elements);
                }
            }
        }

        /// <summary>
        /// Generates all possible combinations - cartesian product - of all elements in defined lists and retrieves the combinations 
        /// as a list of key-value pairs with list names and element value of particular combination.
        /// E.g. a = [1,2,3] b = [4,5,6] c = [7,8,9]
        /// Combinations: 
        /// [(a,1), (b,4), [(c,7)]
        /// [(a,1), (b,4), [(c,8)]
        /// [(a,1), (b,4), [(c,9)]
        /// [(a,1), (b,5), [(c,7)]
        /// [(a,1), (b,5), [(c,8)]
        /// ....
        /// The processing is called "vertical-walkthrough". We start with the first elmenent of first list and then with the first of the next list
        /// untili the last list, where we go through all elements of the last list and create combinations.
        /// </summary>
        /// <param name="level">Current list we are looking in in the process (needed for recursion)</param>
        /// <param name="prevListsCombination">Combination element values of previous lists above the current list to which elements 
        /// of current processing list will be appended as part of combination
        /// </param>
        /// <returns>List of key-value pairs with list name and element value of particular combination</returns>
        public List<Dictionary<string, object>> GenerateListElementCombinations(int level = 0, Dictionary<string, object> prevListsCombination = null)
        {
            /// Needed for unit tests later on
            List<Dictionary<string, object>> combinations = new List<Dictionary<string, object>>();

            /// Recursion termination condition: Check if all lists and its elements are processed
            if (level < this.Count)
            {
                LCTList listItem = this.ElementAt(level);
                string listName = listItem.Name;
                List<object> listElements = listItem.Elements;

                /// If this is the first call to this method, initialize the combination for this "vertical walkthorugh"
                if (prevListsCombination == null)
                {
                    prevListsCombination = new Dictionary<string, object>();
                }

                /// Check if we are processing last list of elements
                if (level < (this.Count - 1))
                {
                    /// If we are processing an intermediate list (non last list) in the list definition, add the elements of the current intermediate list
                    /// to the combination and call this method again to the rest of the following lists in the list definition to trigger "vertical walkthrough"
                    foreach (var listElement in listElements)
                    {
                        //Flat Copy dictionary to avoid referencing
                        Dictionary<string, object> combination = new Dictionary<string, object>(prevListsCombination);
                        combination.Add(listName, listElement);

                        //Recursion until all list levels went through (increase level by 1)
                        combinations.AddRange(this.GenerateListElementCombinations(level + 1, combination));
                    }
                }
                else
                {
                    /// If we are processing last list and its elements, copy the combination of previous lists in the list definition 
                    /// and add elements of the last list creating this way all combinations for one "vertical walkthrough"
                    foreach (var listElement in listElements)
                    {
                        /// Flat Copy dictionary bacuase to avoid referencing
                        Dictionary<string, object> combination = new Dictionary<string, object>(prevListsCombination);
                        /// After the copy of combination of previous lists was created, add the element of the last list to the combination 
                        /// and iterate to the next element to of the last list and add it by looping through
                        combination.Add(listName, listElement);
                        combinations.Add(combination);
                    }
                }
            }

            return combinations;
        }
    }
}
