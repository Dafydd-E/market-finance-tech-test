using Microsoft.Extensions.DependencyInjection;
using SlothEnterprise.ProductApplication.Products.BusinessLoan;
using SlothEnterprise.ProductApplication.Products.ConfidentialInvoiceDiscounts;
using SlothEnterprise.ProductApplication.Products.SelectiveInvoiceDiscounts;

namespace SlothEnterprise.ProductApplication.Products
{
    public static class RegisterServices
    {
        public static IServiceCollection AddProductServices(this IServiceCollection serviceCollection) =>
            serviceCollection
                .AddTransient<ICommandService<SubmitSelectiveInvoiceDiscountApplicationCommand, IResult>, SelectiveInvoiceDiscountService>()
                .AddTransient<ICommandService<SubmitConfidentialInvoiceDiscountCommand, IResult>, ConfidentialInvoiceDiscountService>()
                .AddTransient<ICommandService<SubmitBusinessLoansCommand, IResult>, BusinessLoansService>();
    }
}