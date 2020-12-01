using System;
using SlothEnterprise.ProductApplication.Applications;
using SlothEnterprise.ProductApplication.Products;

namespace SlothEnterprise.ProductApplication
{
    public class ProductApplicationService
    {
        private readonly IProductServiceFactory serviceFactory;

        public ProductApplicationService(IProductServiceFactory serviceFactory)
        {
            this.serviceFactory = serviceFactory ?? throw new ArgumentNullException(nameof(serviceFactory));
        }

        public int SubmitApplicationFor(ISellerApplication application)
        {
            var submitCommand = application.ToSubmitCommand();
            var handler = this.serviceFactory.GetHandlerForCommand(submitCommand);

            var result = handler.ExecuteAsync(submitCommand).Result;
            return result.Successful ? result.Data : -1;
        }
    }
}
