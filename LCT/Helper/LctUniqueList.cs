using LCT.Generation.Preparation.Intermediate;
using System;
using System.Collections.Generic;
using System.Linq;

namespace LCT.Helper
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
    }
}
