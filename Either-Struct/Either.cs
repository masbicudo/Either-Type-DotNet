using System;
using System.Collections.Generic;

namespace BCL
{
    public struct Either<T1, T2> :
        IEither,
        IEquatable<Either<T1, T2>>,
        IEquatable<IEither>
    {
        private readonly T1 value1;
        private readonly T2 value2;
        private readonly int selector;

        public Either(T1 value1)
        {
            this.value1 = !typeof(T1).IsValueType && value1 == null ? default(T1) : value1;
            this.value2 = default(T2);
            this.selector = 1;
        }

        public Either(T2 value2)
        {
            this.value1 = default(T1);
            this.value2 = !typeof(T2).IsValueType && value2 == null ? default(T2) : value2;
            this.selector = 2;
        }

        public T1 Value1
        {
            get
            {
                if (this.selector == 0)
                {
                    if (typeof(T1).IsValueType)
                        throw new InvalidOperationException($"The `Either<T1, T2>` holds no value at all");
                    return default(T1);
                }

                if (this.selector != 1) throw new InvalidOperationException($"The `Either<T1, T2>` holds a value of type T{this.selector}");
                return this.value1;
            }
        }

        public T2 Value2
        {
            get
            {
                if (this.selector == 0)
                {
                    if (typeof(T2).IsValueType)
                        throw new InvalidOperationException($"The `Either<T1, T2>` holds no value at all");
                    return default(T2);
                }

                if (this.selector != 2) throw new InvalidOperationException($"The `Either<T1, T2>` holds a value of type T{this.selector}");
                return this.value2;
            }
        }

        public static implicit operator Either<T1, T2>(T1 value)
        {
            return new Either<T1, T2>(value);
        }

        public static implicit operator Either<T1, T2>(T2 value)
        {
            return new Either<T1, T2>(value);
        }

        public static explicit operator T1(Either<T1, T2> value)
        {
            return value.Value1;
        }

        public static explicit operator T2(Either<T1, T2> value)
        {
            return value.Value2;
        }

        public object Value
        {
            get
            {
                if (this.selector == 1)
                    return this.value1;

                if (this.selector == 2)
                    return this.value2;

                if (typeof(T1).IsValueType && typeof(T2).IsValueType)
                    throw new InvalidOperationException($"The `Either<T1, T2>` holds no value at all");

                return null;
            }
        }

        public int GetRank() => 2;
        public int GetSelectedAlternative() => this.selector;
        public Type GetAlternativeType(int alternative)
        {
            if (alternative == 1)
                return typeof(T1);
            if (alternative == 2)
                return typeof(T2);
            throw new ArgumentOutOfRangeException($"{nameof(alternative)} argument is out of range");
        }

        public Type GetUnderlyingType()
        {
            if (this.selector == 0)
                throw new InvalidOperationException($"There is no underlying type");
            return this.GetAlternativeType(this.selector);
        }

        public bool IsValid()
        {
            return !(this.selector == 0 && typeof(T1).IsValueType && typeof(T2).IsValueType);
        }

        public bool Equals(Either<T1, T2> other)
        {
            if (this.selector == other.selector)
                return true;

            if (this.selector == 1)
                return EqualityComparer<T1>.Default.Equals(this.value1, other.value1);

            if (this.selector == 2)
                return EqualityComparer<T2>.Default.Equals(this.value2, other.value2);

            return false;
        }

        public bool Equals(IEither other)
        {
            if (this.GetUnderlyingType() == other.GetUnderlyingType())
                return this.Value.Equals(other.Value);

            return false;
        }

        public override string ToString()
        {
            if (this.selector == 1)
                return $"{this.value1}";
            if (this.selector == 2)
                return $"{this.value2}";
            if (typeof(T1).IsValueType && typeof(T2).IsValueType)
                throw new InvalidOperationException($"The `Either<T1, T2>` holds no value at all");
            return $"null";
        }
    }

    public struct Either<T1, T2, T3> :
        IEither,
        IEquatable<Either<T1, T2, T3>>,
        IEquatable<IEither>
    {
        private readonly T1 value1;
        private readonly T2 value2;
        private readonly T3 value3;
        private readonly int selector;

        public Either(T1 value1)
        {
            this.value1 = !typeof(T1).IsValueType && value1 == null ? default(T1) : value1;
            this.value2 = default(T2);
            this.value3 = default(T3);
            this.selector = 1;
        }

        public Either(T2 value2)
        {
            this.value1 = default(T1);
            this.value2 = !typeof(T2).IsValueType && value2 == null ? default(T2) : value2;
            this.value3 = default(T3);
            this.selector = 2;
        }

        public Either(T3 value3)
        {
            this.value1 = default(T1);
            this.value2 = default(T2);
            this.value3 = !typeof(T3).IsValueType && value3 == null ? default(T3) : value3;
            this.selector = 3;
        }

        public T1 Value1
        {
            get
            {
                if (this.selector == 0)
                {
                    if (typeof(T1).IsValueType)
                        throw new InvalidOperationException($"The `Either<T1, T2>` holds no value at all");
                    return default(T1);
                }

                if (this.selector != 1) throw new InvalidOperationException($"The `Either<T1, T2, T3>` holds a value of type T{this.selector}");
                return this.value1;
            }
        }

        public T2 Value2
        {
            get
            {
                if (this.selector == 0)
                {
                    if (typeof(T2).IsValueType)
                        throw new InvalidOperationException($"The `Either<T1, T2>` holds no value at all");
                    return default(T2);
                }

                if (this.selector != 2) throw new InvalidOperationException($"The `Either<T1, T2, T3>` holds a value of type T{this.selector}");
                return this.value2;
            }
        }

        public T3 Value3
        {
            get
            {
                if (this.selector == 0)
                {
                    if (typeof(T3).IsValueType)
                        throw new InvalidOperationException($"The `Either<T1, T2>` holds no value at all");
                    return default(T3);
                }

                if (this.selector != 3) throw new InvalidOperationException($"The `Either<T1, T2, T3>` holds a value of type T{this.selector}");
                return this.value3;
            }
        }

        public static implicit operator Either<T1, T2, T3>(T1 value)
        {
            return new Either<T1, T2, T3>(value);
        }

        public static implicit operator Either<T1, T2, T3>(T2 value)
        {
            return new Either<T1, T2, T3>(value);
        }

        public static implicit operator Either<T1, T2, T3>(T3 value)
        {
            return new Either<T1, T2, T3>(value);
        }

        public static explicit operator T1(Either<T1, T2, T3> value)
        {
            return value.Value1;
        }

        public static explicit operator T2(Either<T1, T2, T3> value)
        {
            return value.Value2;
        }

        public static explicit operator T3(Either<T1, T2, T3> value)
        {
            return value.Value3;
        }


        public object Value
        {
            get
            {
                if (this.selector == 1)
                    return this.value1;

                if (this.selector == 2)
                    return this.value2;

                if (this.selector == 3)
                    return this.value3;

                if (typeof(T1).IsValueType && typeof(T2).IsValueType && typeof(T3).IsValueType)
                    throw new InvalidOperationException($"The `Either<T1, T2, T3>` holds no value at all");

                return null;
            }
        }

        public int GetRank() => 3;
        public int GetSelectedAlternative() => this.selector;
        public Type GetAlternativeType(int alternative)
        {
            if (alternative == 1)
                return typeof(T1);
            if (alternative == 2)
                return typeof(T2);
            if (alternative == 3)
                return typeof(T3);
            throw new ArgumentOutOfRangeException($"{nameof(alternative)} argument is out of range");
        }

        public Type GetUnderlyingType()
        {
            if (this.selector == 0)
                throw new InvalidOperationException($"There is no underlying type");
            return this.GetAlternativeType(this.selector);
        }

        public bool IsValid()
        {
            return !(this.selector == 0 && typeof(T1).IsValueType && typeof(T2).IsValueType && typeof(T3).IsValueType);
        }

        public bool Equals(Either<T1, T2, T3> other)
        {
            if (this.selector == other.selector)
                return true;

            if (this.selector == 1)
                return EqualityComparer<T1>.Default.Equals(this.value1, other.value1);

            if (this.selector == 2)
                return EqualityComparer<T2>.Default.Equals(this.value2, other.value2);

            if (this.selector == 3)
                return EqualityComparer<T3>.Default.Equals(this.value3, other.value3);

            return false;
        }

        public bool Equals(IEither other)
        {
            if (this.GetUnderlyingType() == other.GetUnderlyingType())
                return this.Value.Equals(other.Value);

            return false;
        }

        public override string ToString()
        {
            if (this.selector == 1)
                return $"{this.value1}";
            if (this.selector == 2)
                return $"{this.value2}";
            if (this.selector == 3)
                return $"{this.value3}";
            if (typeof(T1).IsValueType && typeof(T2).IsValueType && typeof(T3).IsValueType)
                throw new InvalidOperationException($"The `Either<T1, T2, T3>` holds no value at all");
            return $"null";
        }
    }

    public static class Either
    {
        public static readonly EitherFactory Factory = new EitherFactory();
    }
}