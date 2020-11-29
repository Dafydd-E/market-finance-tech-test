using System;
using System.Threading.Tasks;
using SlothEnterprise.External.V1;

namespace SlothEnterprise.ProductApplication.Products.SelectiveInvoiceDiscounts
{
    public class SelectiveInvoiceDiscountService : ICommandService<SubmitApplicationCommand, SubmitApplicationResult>
    {
        private readonly ISelectInvoiceService selectInvoiceService;

        public SelectiveInvoiceDiscountService(ISelectInvoiceService selectInvoiceService)
        {
            this.selectInvoiceService = selectInvoiceService ?? throw new ArgumentNullException(nameof(selectInvoiceService));
        }

        public Task<SubmitApplicationResult> ExecuteAsync(SubmitApplicationCommand command)
        {
            try
            {
                var result = selectInvoiceService.SubmitApplicationFor(command.CompanyNumber.ToString(), command.InvoiceAmount, command.AdvancePercentage);
                return Task.FromResult(SubmitApplicationResult.Success(result));
            }
            catch (Exception e)
            {
                // exceptions could be thrown for network related errors or infrastructure exceptions
                return Task.FromResult(SubmitApplicationResult.Failure(exception: e));
            }
        }
    }
}