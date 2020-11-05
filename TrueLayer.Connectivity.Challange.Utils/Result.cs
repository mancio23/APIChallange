using System;
using System.Collections.Generic;
using System.Linq;

namespace TrueLayer.Connectivity.Challange.Utils
{
    public class Result<T>
    {
        public bool IsSuccess => Errors.Count == 0;
        public T Value { get; }
        public IReadOnlyCollection<string> Errors { get; }

        private Result(T value, IEnumerable<string> errors)
        {
            Value = value;
            Errors = errors.ToList();
        }

        public static Result<T> Success(T value) =>
            new Result<T>(value, Enumerable.Empty<string>());

        public static Result<T> Error(string error) =>
            Error(new[] { error });

        public static Result<T> Error(IEnumerable<string> errors)
        {
            var errorList = errors.ToList();
            if (errorList.Count == 0)
            {
                throw new ArgumentException(nameof(errors));
            }

            return new Result<T>(default, errorList);
        }

        public override string ToString() =>
            IsSuccess
            ? Value.ToString()
            : string.Join(",", Errors);
    }
}
