using System;
using SlothEnterprise.ProductApplication.Applications;

namespace SlothEnterprise.ProductApplication.Products.ConfidentialInvoiceDiscounts
{
    public class SubmitConfidentialInvoiceDiscountCommand
    {
        public SubmitConfidentialInvoiceDiscountCommand(
            decimal totalLedgerNetworth,
            decimal advancePercentage,
            decimal vatRate,
            ISellerCompanyData companyData)
        {
            this.TotalLedgerNetworth = totalLedgerNetworth;
            this.AdvancePercentage = advancePercentage;
            this.VatRate = vatRate;
            this.CompanyData = companyData ?? throw new ArgumentNullException(nameof(companyData));
        }

        public decimal TotalLedgerNetworth { get; }

        public decimal AdvancePercentage { get; }

        public decimal VatRate { get; }

        public ISellerCompanyData CompanyData { get; }
    }
}