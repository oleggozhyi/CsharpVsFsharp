using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Csharp
{
    public interface IResult<TValue, TError>
    {
    }
    public class Success<TValue, TError> : IResult<TValue, TError>
    {
        public Success(TValue value) => Value = value;
        public TValue Value { get; }
        public override string ToString()
        {
            return "Success " + Value;
        }
    }
    public class Failure<TValue, TError> : IResult<TValue, TError>
    {
        public Failure(TError error) => Error = error;
        public TError Error { get; }
        public override string ToString()
        {
            return "Failure " + Error;
        }
    }

    public static class Result
    {
        public static ArgumentOutOfRangeException OutOfRange() => new ArgumentOutOfRangeException();

        public static IResult<TValue, TError> ToSuccess<TValue, TError>(this TValue value)
            => new Success<TValue, TError>(value);

        public static IResult<TValue, TError> ToFailureResult<TValue, TError>(this TError error)
            => new Failure<TValue, TError>(error);

        public static IResult<TB, TError> Bind<TA, TB, TError>(this IResult<TA, TError> result,
                 Func<TA, IResult<TB, TError>> function)
        {
            Console.WriteLine(result);
            return result is Success<TA, TError> succes ? function(succes.Value)
                  : result is Failure<TA, TError> failure ? failure.Error.ToFailureResult<TB, TError>()
                  : throw OutOfRange();
        }

        public static IResult<TB, TError> MapSuccess<TA, TB, TError>(this IResult<TA, TError> result,
            Func<TA, TB> function) => result.Bind(x => function(x).ToSuccess<TB, TError>());

    }

    public static class ResultLinqExtensions
    {
        public static IResult<TB, TError> SelectMany<TA, TB, TError>(this IResult<TA, TError> result,
            Func<TA, IResult<TB, TError>> function) => result.Bind(function);

        public static IResult<TC, TError> SelectMany<TA, TB, TC, TError>(this IResult<TA, TError> result,
                    Func<TA, IResult<TB, TError>> function, Func<TA, TB, TC> composer) =>
            result.Bind(v1 => function(v1).Select(v2 => composer(v1, v2)));

        public static IResult<TB, TError> Select<TA, TB, TError>(this IResult<TA, TError> result,
            Func<TA, TB> function) => result.MapSuccess(function);
    }
}
