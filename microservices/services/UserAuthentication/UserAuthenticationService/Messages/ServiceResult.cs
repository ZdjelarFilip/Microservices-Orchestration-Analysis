namespace UserAuthenticationService.Messages
{
    public class ServiceResult
    {
        public bool Success { get; protected set; }
        public string Message { get; protected set; }

        protected ServiceResult(bool success, string message, object? data)
        {
            Success = success;
            Message = message;
        }

        public static ServiceResult SuccessResult(string message = "")
        {
            return new ServiceResult(true, message, null);
        }

        public static ServiceResult FailureResult(string message)
        {
            return new ServiceResult(false, message, null);
        }
    }
}