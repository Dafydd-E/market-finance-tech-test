using System;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using SlothEnterprise.External;
using SlothEnterprise.External.V1;
using SlothEnterprise.ProductApplication.Products;

namespace SlothEnterprise.ProductApplication.Tests.Data.Builders
{
    public class ProductApplicationServiceBuilder
    {
        private readonly IServiceCollection serviceCollection = new ServiceCollection();
        private readonly Mock<IConfidentialInvoiceService> confidentialInvoiceServiceMock = new Mock<IConfidentialInvoiceService>();
        private readonly Mock<IBusinessLoansService> businessLoansService = new Mock<IBusinessLoansService>();
        private readonly Mock<ISelectInvoiceService> selectInvoiceService = new Mock<ISelectInvoiceService>();

        public ProductApplicationServiceBuilder WithConfidentialInvoiceResult(IApplicationResult result)
        {
            this.confidentialInvoiceServiceMock
                .Setup(
                    m => m.SubmitApplicationFor(
                        It.IsAny<CompanyDataRequest>(),
                        It.IsAny<decimal>(),
                        It.IsAny<decimal>(),
                        It.IsAny<decimal>()))
                .Returns(result);

            this.serviceCollection.AddSingleton(typeof(IConfidentialInvoiceService), this.confidentialInvoiceServiceMock.Object);
            return this;
        }

        public ProductApplicationServiceBuilder WithBusinessLoansResult(IApplicationResult result)
        {
            this.businessLoansService
                .Setup(
                    m => m.SubmitApplicationFor(
                        It.IsAny<CompanyDataRequest>(),
                        It.IsAny<LoansRequest>()))
                .Returns(result);

            this.serviceCollection.AddSingleton(typeof(IBusinessLoansService), this.businessLoansService.Object);
            return this;
        }

        public ProductApplicationServiceBuilder WithSelectInvoiceResult(int result)
        {
            this.selectInvoiceService
                .Setup(
                    m => m.SubmitApplicationFor(
                        It.IsAny<string>(),
                        It.IsAny<decimal>(),
                        It.IsAny<decimal>()))
                .Returns(result);

            this.serviceCollection.AddSingleton(typeof(ISelectInvoiceService), this.selectInvoiceService.Object);

            return this;
        }

        public ProductApplicationServiceBuilder WithSelectInvoiceException(Exception exception)
        {
            this.selectInvoiceService
                .Setup(
                    m => m.SubmitApplicationFor(
                        It.IsAny<string>(),
                        It.IsAny<decimal>(),
                        It.IsAny<decimal>()))
                .Throws(exception);

            this.serviceCollection.AddSingleton(typeof(ISelectInvoiceService), this.selectInvoiceService.Object);

            return this;
        }

        public ProductApplicationService Build() => new ProductApplicationService(
                new ProductServiceFactory(this.serviceCollection.AddProductServices().BuildServiceProvider()));
    }
}