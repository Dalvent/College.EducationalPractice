using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab4StringLib
{
    public static class StringHelper
    {
        public static string RemoveSymobols(string string1, string igonerSymbols)
        {
            var result = string1;
            foreach (var symbols in igonerSymbols)
                result = result.Replace(symbols.ToString(), "");
            return result;
        }

        public static string RemoveReapeatSymobols(string value, string targetSymbols)
        {
            int removedSymbolsCount = 0;
            int reapetCount = 0;
            string result = value;
            for (int i = 0; i < value.Length; i++)
            {
                char pastSymbol = i == 0 ? '\0' : value[i - 1];
                if (pastSymbol == value[i])
                {
                    reapetCount++;
                    if (i != value.Length - 1) continue;
                    else i++;
                }
                if (reapetCount > 1 && targetSymbols.Contains(pastSymbol))
                {
                    result = result.Remove(i - reapetCount - removedSymbolsCount, reapetCount);
                    removedSymbolsCount += reapetCount;
                }
                reapetCount = 1;
            }
            return result;
        }
    }
}
