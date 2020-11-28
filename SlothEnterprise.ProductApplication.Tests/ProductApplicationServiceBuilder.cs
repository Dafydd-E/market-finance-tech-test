using Moq;
using SlothEnterprise.External;
using SlothEnterprise.External.V1;

namespace SlothEnterprise.ProductApplication.Tests
{
    public class ProductApplicationServiceBuilder
    {
        private readonly Mock<IConfidentialInvoiceService> confidentialInvoiceServiceMock = new Mock<IConfidentialInvoiceService>();
        private readonly Mock<IBusinessLoansService> businessLoansService = new Mock<IBusinessLoansService>();
        private readonly Mock<ISelectInvoiceService> selectInvoiceService = new Mock<ISelectInvoiceService>();

        private IApplicationResult result;

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

            return this;
        }

        public ProductApplicationService Build()
        {
            return new ProductApplicationService(
                this.selectInvoiceService.Object,
                this.confidentialInvoiceServiceMock.Object,
                this.businessLoansService.Object);
        }
    }
}