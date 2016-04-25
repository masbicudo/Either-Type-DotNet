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
            this.propertyNames = new[] { "Alt" }.Concat(this.propertyTypes.Select((x, i) => Regex.Replace(x.Name, @"\W+", "_"))).ToArray();
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
                throw new Exception("Must pass an Either struct");

            var selectedAlt = either.GetSelectedAlternative();
            var selectedValue = either.Value;
            NHibernateUtil.Int32.NullSafeSet(cmd, selectedAlt, index, session);
            for (int it = 0; it < either.GetRank(); it++)
            {
                this.propertyTypes[selectedAlt].NullSafeSet(
                    cmd,
                    it == selectedAlt ? selectedValue : null,
                    index + selectedAlt,
                    session);
            }
        }

        public object DeepCopy(object value)
        {
            // TODO: not sure what to do
            return value;
        }

        public object Disassemble(object value, ISessionImplementor session)
        {
            // TODO: not sure what to do... not many info about this method
            return value;
        }

        public object Assemble(object cached, ISessionImplementor session, object owner)
        {
            // TODO: not sure what to do... not many info about this method
            return cached;
        }

        public object Replace(object original, object target, ISessionImplementor session, object owner)
        {
            // TODO: not sure what to do
            return original;
        }

        public string[] PropertyNames => this.propertyNames.ToArray();

        public IType[] PropertyTypes => this.propertyTypes.ToArray();

        public Type ReturnedClass => typeof(IEither);

        public bool IsMutable => false;
    }
}