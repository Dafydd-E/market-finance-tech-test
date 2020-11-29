namespace SlothEnterprise.ProductApplication.Products.SelectiveInvoiceDiscounts
{
    public class SubmitApplicationCommand
    {
        public SubmitApplicationCommand(decimal invoiceAmount, decimal advantagePercentage, int companyNumber)
        { 
            this.InvoiceAmount = invoiceAmount;
            this.AdvancePercentage = advantagePercentage;
            this.CompanyNumber = companyNumber;
        }

        public decimal InvoiceAmount { get; }

        public decimal AdvancePercentage { get; }

        public int CompanyNumber { get; }
    }
}