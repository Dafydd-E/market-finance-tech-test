using SlothEnterprise.ProductApplication.Applications;
using SlothEnterprise.ProductApplication.Products;

namespace SlothEnterprise.ProductApplication.Tests.Data.Builders
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
            var application = new SellerApplication
            {
                CompanyData = this.sellerCompanyData,
                Product = this.product,
            };

            return application;
        }
    }
}