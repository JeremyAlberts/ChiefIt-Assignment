namespace YakShop.Core.Operation
{
    public class OperationResult<TResult>
    {
        public bool IsSuccess { get; private set; }
        public TResult? Data { get; private set; }
        public string? ErrorMessage { get; private set; }
        public Exception? Exception { get; private set; }
        public OperationStatus OperationStatus { get; private set; }

        public static OperationResult<TResult> Success(TResult result, OperationStatus status)
        {
            return new OperationResult<TResult>
            {
                IsSuccess = true,
                Data = result,
                OperationStatus = status
            };
        }

        public static OperationResult<TResult> Failure(Exception exception, OperationStatus status)
        {
            return new OperationResult<TResult>
            {
                IsSuccess = false,
                ErrorMessage = exception.Message,
                Exception = exception,
                OperationStatus = status
            };
        }
    }
}
