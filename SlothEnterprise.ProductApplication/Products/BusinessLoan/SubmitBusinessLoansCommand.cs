using System;
using SlothEnterprise.ProductApplication.Applications;

namespace SlothEnterprise.ProductApplication.Products.BusinessLoan
{
    public class SubmitBusinessLoansCommand
    {
        public SubmitBusinessLoansCommand(
            ISellerCompanyData companyData,
            decimal interestRatePerAnnum,
            decimal loanAmount)
        {
            this.CompanyData = companyData ?? throw new ArgumentNullException(nameof(companyData));
            this.InterestRatePerAnnum = interestRatePerAnnum;
            this.LoanAmount = loanAmount;
        }

        public ISellerCompanyData CompanyData { get; }

        public decimal InterestRatePerAnnum { get; }

        public decimal LoanAmount { get; }
    }
}