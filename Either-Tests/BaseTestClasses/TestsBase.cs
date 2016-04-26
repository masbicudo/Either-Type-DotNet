using System;
using System.Threading;
using JetBrains.Annotations;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Either_Tests.BaseTestClasses
{
    public class TestsBase
    {
        private static int globalInstanceId;
        private readonly int instanceId;

        public TestsBase()
        {
            this.instanceId = Interlocked.Increment(ref globalInstanceId);
        }

        public override string ToString()
        {
            return $"{this.GetType().Name}({this.instanceId})";
        }

        protected static void AssertThrows<TEx>([NotNull] Action action)
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

        protected static void AssertThrows<TEx>([NotNull] Func<object> action)
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

        protected static void AssertThrows([NotNull] Action action)
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

        protected static void AssertThrows([NotNull] Func<object> action)
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