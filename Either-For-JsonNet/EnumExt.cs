using System;
using System.Collections.Generic;

namespace Either_For_JsonNet
{
    internal static class EnumExt
    {
        public static T WithMax<T>(this IEnumerable<T> enumerable, Func<T, int?> valueGetter)
        {
            int? max = null;
            T maxItem = default(T);
            foreach (var item in enumerable)
            {
                var val = valueGetter(item);
                if (max == null)
                {
                    max = val;
                    maxItem = item;
                }
                else if (max < val)
                {
                    max = val;
                    maxItem = item;
                }
            }

            return maxItem;
        }
    }
}