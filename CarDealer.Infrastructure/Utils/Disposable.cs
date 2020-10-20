using System;

namespace CarDealer.Infrastructure.Utils
{
    internal static class Disposable
    {
        public static TResult Using<TDisposable, TResult>(Func<TDisposable> factory, Func<TDisposable, TResult> map) where TDisposable : IDisposable
        {
            using (var disposable = factory())
            {
                return map(disposable);
            }
        }
    }
}
