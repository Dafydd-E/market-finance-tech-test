using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using SlothEnterprise.External;
using SlothEnterprise.External.V1;

namespace SlothEnterprise.ProductApplication.Products.BusinessLoan
{
    public class BusinessLoansService :
        ICommandService<SubmitBusinessLoansCommand, IResult>
    {
        private readonly IBusinessLoansService businessLoansService;

        public BusinessLoansService(IBusinessLoansService businessLoansService)
        {
            this.businessLoansService = businessLoansService ?? throw new ArgumentNullException(nameof(businessLoansService));
        }

        public Task<IResult> ExecuteAsync(SubmitBusinessLoansCommand command)
        {
            var result = this.businessLoansService.SubmitApplicationFor(
                new CompanyDataRequest
                {
                    CompanyFounded = command.CompanyData.Founded,
                    CompanyNumber = command.CompanyData.Number,
                    CompanyName = command.CompanyData.Name,
                    DirectorName = command.CompanyData.DirectorName
                }, new LoansRequest
                {
                    InterestRatePerAnnum = command.InterestRatePerAnnum,
                    LoanAmount = command.LoanAmount
                });

            return (result.Success)
                ? result.ApplicationId.HasValue
                    ? Task.FromResult(SubmitApplicationResult.Success(result.ApplicationId.Value) as IResult)
                    : Task.FromResult(SubmitApplicationResult.Failure(message: "") as IResult)
                : Task.FromResult(SubmitApplicationResult.Failure(message: $"Request to businessLoansService failed\n{string.Join("\n", result.Errors ?? new List<string>())}") as IResult);
        }
    }
}