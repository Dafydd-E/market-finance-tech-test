using System;
using Microsoft.Extensions.DependencyInjection;

namespace SlothEnterprise.ProductApplication.Products
{
    public class ProductServiceFactory : IProductServiceFactory
    {
        private readonly IServiceProvider serviceProvider;

        public ProductServiceFactory(IServiceProvider serviceProvider)
        {
            this.serviceProvider = serviceProvider ?? throw new ArgumentNullException(nameof(serviceProvider));
        }

        public ICommandService<TCommand, IResult> GetHandlerForCommand<TCommand>(TCommand product)
        {
            return this.serviceProvider.GetService<ICommandService<TCommand, IResult>>() 
                ?? throw new InvalidOperationException($"No registered product service for command: {typeof(TCommand)}");
        }
    }
}