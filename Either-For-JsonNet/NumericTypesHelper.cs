using System;
using System.Collections.Generic;

namespace Either_For_JsonNet
{
    internal static class NumericTypesHelper
    {
        private static readonly Dictionary<Type, int> intSizes = new Dictionary<Type, int>
            {
                { typeof(int), sizeof(int) },
                { typeof(uint), sizeof(uint) },
                { typeof(long), sizeof(long) },
                { typeof(ulong), sizeof(ulong) },
                { typeof(short), sizeof(short) },
                { typeof(ushort), sizeof(ushort) },
                { typeof(byte), sizeof(byte) },
                { typeof(sbyte), sizeof(sbyte) },
            };

        private static readonly Dictionary<Type, int> floatSizes = new Dictionary<Type, int>
            {
                { typeof(float), sizeof(float) },
                { typeof(double), sizeof(double) },
            };

        public static int? GetIntegerSize(Type integerType)
        {
            int size;
            intSizes.TryGetValue(integerType, out size);
            return size > 0 ? size : null as int?;
        }

        public static int? GetFloatSize(Type floatType)
        {
            int size;
            floatSizes.TryGetValue(floatType, out size);
            return size > 0 ? size : null as int?;
        }
    }
}