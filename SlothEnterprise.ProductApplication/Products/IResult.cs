using System;

namespace SlothEnterprise.ProductApplication.Products
{
    public interface IResult
    {
        int Data { get; }

        bool Successful { get; }

        Exception Error { get; }

        string Message { get; }
    }
}