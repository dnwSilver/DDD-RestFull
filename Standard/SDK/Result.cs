using System;
using Standard.RestFull;

namespace Standard.SDK
{
    public struct Result
    {
        internal bool Success { get; }
        internal bool Failure { get; }
        internal static Result Combine(Result resultSave1, Result resultSave2)
        {
            throw new NotImplementedException();
        }

        internal IResponse MapToResponse()
        {
            throw new NotImplementedException();
        }
    }
}