using Moq;
using SlothEnterprise.ProductApplication.Applications;
using SlothEnterprise.ProductApplication.Products;

namespace SlothEnterprise.ProductApplication.Tests
{
    public class ApplicationBuilder
    {
        private IProduct product;
        private ISellerCompanyData sellerCompanyData = new SellerCompanyData();

        public ApplicationBuilder WithProduct(IProduct product)
        {
            this.product = product;

            return this;
        }

        public ApplicationBuilder WithSellerCompanyData(ISellerCompanyData companyData)
        {
            this.sellerCompanyData = companyData;
            return this;
        }

        public ISellerApplication Build()
        {
            var sellerApplicationMock = new Mock<ISellerApplication>();
            sellerApplicationMock.SetupProperty(p => p.Product, this.product);
            sellerApplicationMock.SetupProperty(p => p.CompanyData, this.sellerCompanyData);

            return sellerApplicationMock.Object;
        }
    }
}