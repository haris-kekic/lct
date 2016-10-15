using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LCT.Library
{
    public class DictionaryCombinationsComparer : IEqualityComparer<Dictionary<string, object>>
    {
        public bool Equals(Dictionary<string, object> xDic, Dictionary<string, object> yDic)
        {
            bool isEqual = true;

            foreach(var itemOfXDic in xDic)
            {
                if (!yDic.ContainsKey(itemOfXDic.Key) || (yDic[itemOfXDic.Key].GetHashCode() != itemOfXDic.Value.GetHashCode()))
                {
                    isEqual = false;
                    break;
                } 
            }

            return isEqual;
        }

        public int GetHashCode(Dictionary<string, object> obj)
        {
            return obj.GetHashCode();
        }
    }
}
