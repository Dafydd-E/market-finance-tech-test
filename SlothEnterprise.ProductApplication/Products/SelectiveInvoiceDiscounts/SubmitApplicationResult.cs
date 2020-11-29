using System;

namespace SlothEnterprise.ProductApplication.Products.SelectiveInvoiceDiscounts
{
    public class SubmitApplicationResult
    {
        private SubmitApplicationResult(int result)
        {
            this.Successful = true;
            this.Result = result;
        }

        private SubmitApplicationResult(string message, Exception exception)
        {
            this.Message = message;
            this.Error = exception;
            this.Successful = false;
        }

        public int Result { get; }

        public bool Successful { get; }

        public Exception Error { get; }

        public string Message { get; }

        public static SubmitApplicationResult Success(int result)
        {
            return new SubmitApplicationResult(result);
        }

        public static SubmitApplicationResult Failure(string reason = null, Exception exception = null)
        {
            return new SubmitApplicationResult(reason, exception);
        }
    }
}