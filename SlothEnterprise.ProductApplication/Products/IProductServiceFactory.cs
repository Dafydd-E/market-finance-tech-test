using System.Collections.Generic;

namespace SlothEnterprise.ProductApplication.Products
{
    public interface IProductServiceFactory
    {
        ICommandService<TCommand, IResult> GetHandlerForCommand<TCommand>(TCommand product);
    }
}