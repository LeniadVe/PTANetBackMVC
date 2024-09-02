using AdministrationAPI.Controllers;
using Microsoft.AspNetCore.Mvc;

namespace AdministrationAPI.Errors
{
    internal static class ResultHelper
    {
        /// <summary>
        /// Executes a given function and handles any exceptions that occur during execution.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="func"></param>
        /// <param name="errorMessage"></param>
        /// <returns></returns>
        internal static Result<T> Execute<T>(Func<Result<T>> func, ILogger logger, string errorMessage)
        {
            try
            {
                return func();
            }
            catch (Exception ex)
            {
                logger.LogError($"{errorMessage}: {ex.Message}");
                return Result<T>.Failure($"{errorMessage}: {ex.Message}");
            }
        }

        /// <summary>
        /// Handles the result of an operation, mapping it to an appropriate IActionResult response.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="result"></param>
        /// <param name="logger"></param>
        /// <param name="okFunc"></param>
        /// <param name="errorFunc"></param>
        /// <param name="statusCode"></param>
        /// <param name="errorMessage"></param>
        /// <returns></returns>
        internal static IActionResult HandleResult<T>(Result<T> result, ILogger<FeesController> logger, Func<object, IActionResult> okFunc, Func<int, string, IActionResult> errorFunc, int statusCode, string errorMessage)
        {
            return result
                .Map(ok => okFunc(ok))
                .MapError(error =>
                {
                    logger.LogError(errorMessage);
                    return errorFunc(statusCode, errorMessage);
                })
                .GetValueOrDefault();
        }
    }
}
