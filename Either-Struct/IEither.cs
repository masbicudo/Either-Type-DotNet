using System;

namespace BCL
{
    public interface IEither
    {
        object Value { get; }
        int GetRank();
        int GetSelectedAlternative();
        Type GetAlternativeType(int alternative);
        Type GetUnderlyingType();
        bool IsValid();
    }
}