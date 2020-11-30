using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using SlothEnterprise.External;
using SlothEnterprise.External.V1;

namespace SlothEnterprise.ProductApplication.Products.ConfidentialInvoiceDiscounts
{
    public class ConfidentialInvoiceDiscountService :
        ICommandService<SubmitConfidentialInvoiceDiscountCommand, IResult>
    {
        private readonly IConfidentialInvoiceService confidentialInvoiceService;

        public ConfidentialInvoiceDiscountService(IConfidentialInvoiceService confidentialInvoiceService)
        {
            this.confidentialInvoiceService = confidentialInvoiceService ?? throw new ArgumentNullException(nameof(confidentialInvoiceService));
        }

        public Task<IResult> ExecuteAsync(SubmitConfidentialInvoiceDiscountCommand command)
        {
            var result = this.confidentialInvoiceService.SubmitApplicationFor(
                new CompanyDataRequest
                {
                    CompanyFounded = command.CompanyData.Founded,
                    CompanyNumber = command.CompanyData.Number,
                    CompanyName = command.CompanyData.Name,
                    DirectorName = command.CompanyData.DirectorName
                }, 
                command.TotalLedgerNetworth,
                command.AdvancePercentage,
                command.VatRate);

            return (result.Success) 
                ? result.ApplicationId.HasValue 
                    ? Task.FromResult(SubmitApplicationResult.Success(result.ApplicationId.Value) as IResult) 
                    : Task.FromResult(SubmitApplicationResult.Failure(message: "Web request completed, but result returned null") as IResult)
                : Task.FromResult(SubmitApplicationResult.Failure(message: $"Web request failed\n{string.Join("\n", result.Errors ?? new List<string>())}") as IResult);
        }
    }
}