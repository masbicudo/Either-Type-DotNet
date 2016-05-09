﻿using System;
using JetBrains.Annotations;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Either_Tests.BaseTestClasses
{
    public static class MyAssert
    {
        public static void Throws<TEx>([NotNull] Action action)
            where TEx : Exception
        {
            // ReSharper disable once ConditionIsAlwaysTrueOrFalse
            if (action == null)
                Assert.Inconclusive("Argument null: {0}", nameof(action));

            TEx exception = null;
            try
            {
                action();
            }
            catch (TEx ex)
            {
                exception = ex;
            }
            Assert.IsInstanceOfType(exception, typeof(TEx));
        }

        public static void Throws<TEx>([NotNull] Func<object> action)
            where TEx : Exception
        {
            // ReSharper disable once ConditionIsAlwaysTrueOrFalse
            if (action == null)
                Assert.Inconclusive("Argument null: {0}", nameof(action));

            TEx exception = null;
            try
            {
                action();
            }
            catch (TEx ex)
            {
                exception = ex;
            }
            Assert.IsInstanceOfType(exception, typeof(TEx));
        }

        public static void Throws([NotNull] Action action)
        {
            // ReSharper disable once ConditionIsAlwaysTrueOrFalse
            if (action == null)
                Assert.Inconclusive("Argument null: {0}", nameof(action));

            Exception exception = null;
            try
            {
                action();
            }
            catch (Exception ex)
            {
                exception = ex;
            }
            Assert.IsInstanceOfType(exception, typeof(Exception));
        }

        public static void Throws([NotNull] Func<object> action)
        {
            // ReSharper disable once ConditionIsAlwaysTrueOrFalse
            if (action == null)
                Assert.Inconclusive("Argument null: {0}", nameof(action));

            Exception exception = null;
            try
            {
                action();
            }
            catch (Exception ex)
            {
                exception = ex;
            }
            Assert.IsInstanceOfType(exception, typeof(Exception));
        }
    }
}