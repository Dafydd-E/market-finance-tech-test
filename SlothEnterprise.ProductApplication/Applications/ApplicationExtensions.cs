using System;
using SlothEnterprise.ProductApplication.Products;
using SlothEnterprise.ProductApplication.Products.BusinessLoan;
using SlothEnterprise.ProductApplication.Products.ConfidentialInvoiceDiscounts;
using SlothEnterprise.ProductApplication.Products.SelectiveInvoiceDiscounts;

namespace SlothEnterprise.ProductApplication.Applications
{
    public static class ApplicationExtensions
    { 
        public static dynamic ToSubmitCommand(this ISellerApplication application)
        {
            if (application.Product is SelectiveInvoiceDiscount invoiceDiscount)
            {
                return new SubmitSelectiveInvoiceDiscountApplicationCommand(
                    invoiceDiscount.InvoiceAmount,
                    invoiceDiscount.AdvancePercentage,
                    application.CompanyData.Number);
            }

            if (application.Product is BusinessLoans businessLoan)
            {
                return new SubmitBusinessLoansCommand(
                   application.CompanyData,
                   businessLoan.InterestRatePerAnnum,
                   businessLoan.LoanAmount);
            }

            if (application.Product is ConfidentialInvoiceDiscount confidentialInvoiceDiscount)
            {
                return new SubmitConfidentialInvoiceDiscountCommand(
                    confidentialInvoiceDiscount.TotalLedgerNetworth,
                    confidentialInvoiceDiscount.AdvancePercentage,
                    confidentialInvoiceDiscount.VatRate,
                    application.CompanyData);
            }

            throw new InvalidOperationException();
        }
    }
}