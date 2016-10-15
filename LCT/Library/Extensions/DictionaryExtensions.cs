using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LCT.Library.Extensions
{
    public static class DictionaryExtensions
    {
        public static Dictionary<string, decimal> ToDecimalDictionary(this Dictionary<string, object> sourceDictionary)
        {
            Dictionary<string, decimal> targetDictionary = new Dictionary<string, decimal>();
            
            foreach(var item in sourceDictionary)
            {
                targetDictionary.Add(item.Key, (decimal) item.Value);
            }

            return targetDictionary;
        }
    }
}
