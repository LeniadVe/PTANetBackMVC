using Microsoft.AspNetCore.Mvc;

namespace AdministrationAPI.Errors
{
    public static class ResultExtensions
    {
        public static Result<U> Map<T, U>(this Result<T> result, Func<T, U> mapFunc)
        {
            if (result.IsSuccess)
                return Result<U>.Success(mapFunc(result.Value));

            return Result<U>.Failure(result.Message);
        }

        public static Result<U> MapError<U>(this Result<U> result, Func<string,IActionResult> errorFunc)
        {
            if (result.IsFailure)
            {
                errorFunc(result.Message);
                return Result<U>.Failure(result.Message);
            }
            return Result<U>.Success(result.Value);

        }        
    }
}
