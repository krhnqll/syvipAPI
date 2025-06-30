namespace DataAccess.Helper
{
    public class Result<T> : IResult<T>
    {
        public bool Success { get; private set; }
        public string Message { get; private set; }
        public T Data { get; private set; }

        private Result(bool success, string message, T data)
        {
            Success = success;
            Message = message;
            Data = data;
        }

        // Başarılı sonuç (verili)
        public static Result<T> SuccessResult(T data, string message = "")
        {
            return new Result<T>(true, message, data);
        }

        // Başarılı sonuç (verisiz)
        public static Result<T> SuccessResult(string message = "")
        {
            return new Result<T>(true, message, default);
        }

        // Hatalı sonuç
        public static Result<T> ErrorResult(string message)
        {
            return new Result<T>(false, message, default);
        }
    }
}


