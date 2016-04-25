using BCL;
using NHibernate;
using NHibernate.Engine;
using NHibernate.Type;
using NHibernate.UserTypes;
using System;
using System.Data;
using System.Linq;
using System.Text.RegularExpressions;

namespace Either_For_NHibernate
{
    public class EitherUserType<T> :
        ICompositeUserType
        where T : IEither
    {
        private readonly string[] propertyNames;
        private readonly IType[] propertyTypes;

        public EitherUserType()
        {
            if (!typeof(T).IsGenericType)
                throw new Exception("Must be generic");
            if (typeof(T).Name == "Either")
                throw new Exception("Type must be Either<...>");

            this.propertyTypes = new[] { NHibernateUtil.Int32 }.Concat(typeof(T).GetGenericArguments().Select(NHibernateUtil.GuessType)).ToArray();
            this.propertyNames = new[] { "Alt" }.Concat(this.propertyTypes.Skip(1).Select((x, i) => Regex.Replace(x.Name, @"\W+", "_"))).ToArray();
        }

        public object GetPropertyValue(object component, int property)
        {
            var either = (IEither)component;

            if (property == 0)
                return either.GetSelectedAlternative();

            if (either.GetSelectedAlternative() != property)
                return null;

            return either.Value;
        }

        public void SetPropertyValue(object component, int property, object value)
        {
            throw new NotSupportedException("Either is an immutable struct. SetPropertyValue isn't supported.");
        }

        public new bool Equals(object x, object y)
        {
            if (x == null) return y == null;
            if (y == null) return false;
            return x.Equals(y);
        }

        public int GetHashCode(object x)
        {
            if (x == null) return -917340415;
            return x.GetHashCode();
        }

        public object NullSafeGet(IDataReader dr, string[] names, ISessionImplementor session, object owner)
        {
            if (dr == null)
                return null;

            var selectedAlt = (int)NHibernateUtil.Int32.NullSafeGet(dr, names[0], session, owner);
            var value = this.propertyTypes[selectedAlt].NullSafeGet(dr, names[selectedAlt], session, owner);
            var result = Either.Factory.Create(value, typeof(T));
            return result;
        }

        public void NullSafeSet(IDbCommand cmd, object value, int index, bool[] settable, ISessionImplementor session)
        {
            if (value == null)
                return;

            var either = value as IEither;

            if (either == null)
                throw new Exception("Must pass an `Either<...>` struct");

            var selectedAlt = either.GetSelectedAlternative();
            var selectedValue = either.Value;
            NHibernateUtil.Int32.NullSafeSet(cmd, selectedAlt, index + 0, session);
            for (int it = 1; it <= either.GetRank(); it++)
            {
                this.propertyTypes[it].NullSafeSet(
                    cmd,
                    it == selectedAlt ? selectedValue : null,
                    index + it,
                    session);
            }
        }

        public object DeepCopy(object value, ISessionImplementor session, object owner)
        {
            if (value == null)
                return null;

            var either = value as IEither;

            if (either == null)
                throw new Exception("Must pass an `Either<...>` struct");

            var selectedAlt = either.GetSelectedAlternative();
            var selectedValue = either.Value;
            var clonedValue = this.propertyTypes[selectedAlt].DeepCopy(selectedValue, session?.EntityMode ?? 0, session?.Factory);
            var result = Either.Factory.Create(clonedValue, typeof(T));
            return result;
        }

        public object DeepCopy(object value)
        {
            return this.DeepCopy(value, null, null);
        }

        /// <summary>
        /// Returns a value that will be stored in the NHibernate cache, when cache is enabled.
        /// </summary>
        /// <param name="value"></param>
        /// <param name="session"></param>
        /// <returns></returns>
        public object Disassemble(object value, ISessionImplementor session)
        {
            return this.DeepCopy(value, session, null);
        }

        /// <summary>
        /// Restores an object that has been stored in NHibernate cache, when cache is enabled.
        /// </summary>
        /// <param name="cached"></param>
        /// <param name="session"></param>
        /// <param name="owner"></param>
        /// <returns></returns>
        public object Assemble(object cached, ISessionImplementor session, object owner)
        {
            return this.DeepCopy(cached, session, owner);
        }

        /// <summary>
        /// During merge, replace the existing (target) value in the entity we are merging to
        ///             with a new (original) value from the detached entity we are merging.
        /// </summary>
        /// <param name="original"></param>
        /// <param name="target"></param>
        /// <param name="session"></param>
        /// <param name="owner"></param>
        /// <returns></returns>
        public object Replace(object original, object target, ISessionImplementor session, object owner)
        {
            if (original == null)
                return null;

            var either = original as IEither;

            if (either == null)
                throw new Exception("Must pass an `Either<...>` struct");

            var targetEither = target as IEither;
            var targetSelectedValue = targetEither?.Value;

            var selectedAlt = either.GetSelectedAlternative();
            var selectedValue = either.Value;
            // TODO: don't know what to pass to inner type `Replace` final parameters
            var mergedValue = this.propertyTypes[selectedAlt].Replace(selectedValue, targetSelectedValue, session, owner, null);
            var result = Either.Factory.Create(mergedValue, typeof(T));
            return result;
        }

        public string[] PropertyNames => this.propertyNames.ToArray();

        public IType[] PropertyTypes => this.propertyTypes.ToArray();

        public Type ReturnedClass => typeof(IEither);

        public bool IsMutable => false;
    }
}