using System;
using NHibernate.JsonColumn.JetBrains.Annotations;

namespace BCL
{
    /// <summary>
    /// Interface for the Either.
    /// </summary>
    public interface IEither
    {
        /// <summary>
        /// Gets the value stored in this Either instance.
        /// If any of the type alternatives is nullable,
        /// then it is possible that this Value returns null.
        /// </summary>
        object Value { get; }

        /// <summary>
        /// Gets the number of alternating types this type supports.
        /// </summary>
        /// <returns></returns>
        int GetRank();

        /// <summary>
        /// Gets the selected type alternative that is stored in this object.
        /// A value of 0 (zero) indicates that it is null, when the type is nullable.
        /// </summary>
        /// <returns></returns>
        int GetSelectedAlternative();

        /// <summary>
        /// Gets the type of the indexed alternating type.
        /// Indexes go from 1 to the number returned by `GetRank()`.
        /// </summary>
        /// <param name="alternative"></param>
        /// <returns></returns>
        [NotNull]
        Type GetAlternativeType(int alternative);

        /// <summary>
        /// Gets the type that is currently stored in this Either instance.
        /// This throws an exception when the value equals null.
        /// </summary>
        /// <returns></returns>
        [NotNull]
        Type GetUnderlyingType();

        /// <summary>
        /// Gets a value indicating whether this object is in a valid state.
        /// An invalid instance happens when no types are nullable but the instance was not initialized.
        /// For example: `var x = new Either&lt;Int32, DateTime&gt;();` creates an invalid instance.
        /// </summary>
        /// <returns></returns>
        bool IsValid();

        /// <summary>
        /// Indicates whether this type accepts a null value.
        /// This is only possible when at least on of the Either generic parameters is nullable:
        /// a reference type or a Nullable`1.
        /// </summary>
        /// <returns></returns>
        bool IsNullable();
    }
}