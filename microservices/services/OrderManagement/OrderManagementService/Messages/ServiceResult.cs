namespace OrderManagementService.Messages
{
    public class ServiceResult
    {
        public bool Success { get; protected set; }
        public string Message { get; protected set; }
        public object? Data { get; protected set; }

        protected ServiceResult(bool success, string message, object? data)
        {
            Success = success;
            Message = message;
            Data = data;
        }

        public static ServiceResult SuccessResult(string message = "")
        {
            return new ServiceResult(true, message, null);
        }

        public static ServiceResult FailureResult(string message)
        {
            return new ServiceResult(false, message, null);
        }

        public static ServiceResult<T> SuccessResult<T>(T data, string message = "")
        {
            return new ServiceResult<T>(true, message, data);
        }

        public static ServiceResult<T> FailureResult<T>(string message)
        {
            return new ServiceResult<T>(false, message, default!);
        }
    }

    public class ServiceResult<T> : ServiceResult
    {
        new public T? Data { get; private set; }

        internal ServiceResult(bool success, string message, T? data) : base(success, message, data)
        {
            Data = data;
        }
    }
}