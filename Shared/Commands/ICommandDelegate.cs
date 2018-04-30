using System;
using Shared.WebService;

namespace Shared.Delegates
{
    public interface ICommandDelegate<T>
    {
        Action<Error> OnFailed { get; set; }
        Action OnNotConnection { get; set; }
        Action<T> OnSuccess { get; set; }
    }
}
