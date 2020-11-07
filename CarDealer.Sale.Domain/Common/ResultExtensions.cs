using System;

namespace CarDealer.Domain.Common
{
    public static class ResultExtensions
    {
        public static Result<TSource> ToResult<TSource>(this TSource source)
        {
            return Result.Ok(source);
        }
        public static Result<T> ToResult<T>(this Maybe<T> maybe, string errorMessage) where T : class
        {
            if (maybe.HasNoValue)
                return Result.Fail<T>(errorMessage);

            return Result.Ok(maybe.Value);
        }
        public static T OnBoth<T>(this Result result, Func<Result, T> func)
        {
            return func(result);
        }
        public static Result OnBoth(this Result result, Action<Result> action)
        {
            action(result);

            return result;
        }

        public static Result<T> Ensure<T>(this Result<T> result, Func<T, bool> predicate, string errorMessage)
        {
            if (!predicate(result.Value))
                result.AddError(errorMessage);

            return result;
        }
        public static Result SkipPayload<T>(this Result<T> result)
        {
            return Result.Fail(result.Errors);
        }
        public static Result<K> Map<T, K>(this Result<T> result, Func<T, K> func)
        {
            if (result.IsFailure)
                return Result.Fail<K>(result.Errors);

            return Result.Ok(func(result.Value));
        }

        public static Result OnSuccess(this Result result, Action action)
        {
            if (result.IsSuccess)
                action();
            return result;
        }

        public static Result<T> OnSuccess<T>(this Result<T> result, Action<T> action)
        {
            if (result.IsSuccess)
                action(result.Value);
            return result;
        }

        public static Result OnSuccess(this Result result, Func<Result> func)
        {
            if (result.IsSuccess)
                return func();
            return result;

        }

        public static Result OnFailure(this Result result, Action action)
        {
            if (result.IsFailure)
                action();

            return result;
        }
    }
}
