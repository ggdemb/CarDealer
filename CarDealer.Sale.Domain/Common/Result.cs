using System;
using System.Collections.Generic;
using System.Linq;

namespace CarDealer.Domain.Common
{
    //https://enterprisecraftsmanship.com/posts/functional-c-handling-failures-input-errors/
    public class Result
    {
        public bool IsFailure => Errors.Any();
        public List<string> Errors { get; }
        public bool IsSuccess => !IsFailure;
        protected Result()
        {
            Errors = new List<string>();
        }
        protected Result(List<string> errors)
        {
            Errors = errors;
        }
        protected Result(string error) : this()
        {
            if (error != string.Empty)
                Errors.Add(error);
        }

        public void AddError(string error)
        {
            Errors.Add(error);
        }

        public static Result Fail(List<string> messages)
        {
            return new Result(messages);
        }
        public static Result Fail(string message)
        {
            return new Result(message);
        }

        public static Result<T> Fail<T>(string message)
        {
            return new Result<T>(default(T), message);
        }
        public static Result<T> Fail<T>(List<string> messages)
        {
            return new Result<T>(default(T), messages);
        }

        public static Result Ok()
        {
            return new Result();
        }

        public static Result<T> Ok<T>(T value)
        {
            return new Result<T>(value, string.Empty);
        }
    }


    public class Result<T> : Result
    {
        public T Value { get; }
        protected internal Result(T value, string error)
            : base(error)
        {
            Value = value;
        }
        protected internal Result(T value, List<string> errors)
           : base(errors)
        {
            Value = value;
        }
    }
}
