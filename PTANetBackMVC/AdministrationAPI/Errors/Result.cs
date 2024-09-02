namespace AdministrationAPI.Errors
{
    public class Result<T>
    {
        public T Value { get; }
        public string Message { get; }
        public bool IsSuccess { get; }
        public bool IsFailure => !IsSuccess;

        private Result(T value, string message, bool isSuccess)
        {
            Value = value;
            Message = message;
            IsSuccess = isSuccess;
        }

        public static Result<T> Success(T value) => new(value, default, true);

        public static Result<T> Failure(string msg) => new(default, msg, false);
        public T GetValueOrDefault(T defaultValue = default) => IsSuccess ? Value : defaultValue;       
    }
}
