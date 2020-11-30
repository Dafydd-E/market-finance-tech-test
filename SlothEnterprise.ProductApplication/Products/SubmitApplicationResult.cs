using System;

namespace SlothEnterprise.ProductApplication.Products
{
    public class SubmitApplicationResult : IResult
    {
        private SubmitApplicationResult(int result)
        {
            this.Successful = true;
            this.Data = result;
        }

        private SubmitApplicationResult(string message, Exception exception)
        {
            this.Message = message;
            this.Error = exception;
            this.Successful = false;
        }

        public int Data { get; }

        public bool Successful { get; }

        public Exception Error { get; }

        public string Message { get; }

        public static SubmitApplicationResult Success(int result)
        {
            return new SubmitApplicationResult(result);
        }

        public static SubmitApplicationResult Failure(string message = null, Exception exception = null)
        {
            return new SubmitApplicationResult(message, exception);
        }
    }
}