using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Reflection;

namespace BCL
{
    public class EitherFactory
    {
        private readonly Dictionary<Type, Func<object, IEither>> dicCreators = new Dictionary<Type, Func<object, IEither>>();

        public IEither Create(object value, params Type[] eitherSubtypes)
        {
            var eitherType = GetEitherType(eitherSubtypes);
            return this.Create(value, eitherType);
        }

        private static Type GetEitherType(Type[] eitherSubtypes)
        {
            if (eitherSubtypes.Length == 2)
                return typeof(Either<,>).MakeGenericType(eitherSubtypes);
            if (eitherSubtypes.Length == 3)
                return typeof(Either<,,>).MakeGenericType(eitherSubtypes);
            throw new InvalidOperationException($"There in no `Either<...>` that support {eitherSubtypes.Length} alternatives.");
        }

        public IEither Create(object value, Type eitherType)
        {
            Func<object, IEither> creator;
            lock (this.dicCreators)
            {
                if (!this.dicCreators.TryGetValue(eitherType, out creator))
                {
                    var paramObj = Expression.Parameter(typeof(object), "value");

                    var emptyCtor = eitherType.GetConstructor(new Type[0]);

                    Expression newExpression = null;
                    if (emptyCtor != null)
                        newExpression = Expression.New(emptyCtor);
                    else if (eitherType.IsValueType)
                        newExpression = Expression.Default(eitherType);

                    Expression current;
                    if (newExpression != null)
                    {
                        current = Expression.Condition(
                            Expression.ReferenceEqual(paramObj, Expression.Constant(null)),
                            Expression.Convert(newExpression, typeof(IEither)),
                            Expression.Constant(null, typeof(IEither))
                            );
                    }
                    else
                        current = Expression.Constant(null, typeof(IEither));

                    var eitherSubtypes = eitherType.GetGenericArguments();
                    foreach (var eitherSubtype in eitherSubtypes)
                    {
                        var ctor = eitherType.GetConstructor(types: new[] { eitherSubtype });
                        if (ctor != null)
                            current = Expression.Condition(
                                Expression.TypeIs(paramObj, eitherSubtype),
                                Expression.Convert(Expression.New(ctor, Expression.Convert(paramObj, eitherSubtype)), typeof(IEither)),
                                current);
                    }
                    var body = current;
                    var creatorExpr = Expression.Lambda<Func<object, IEither>>(body, paramObj);
                    this.dicCreators[eitherType] = creator = creatorExpr.Compile();
                }
            }

            var result = creator(value);
            if (object.ReferenceEquals(result, null))
                throw new InvalidCastException("Cannot cast the given object to the target `IEither` type.");

            if (!result.IsValid())
                throw new InvalidCastException("Cannot cast the given object to the target `IEither` type. It does not accept null.");

            return result;
        }
    }
}