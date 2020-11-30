using System;
using System.Threading.Tasks;
using AutoFixture;
using AutoFixture.Xunit2;
using FluentAssertions;
using Moq;
using SlothEnterprise.External;
using SlothEnterprise.External.V1;
using SlothEnterprise.ProductApplication.Applications;
using SlothEnterprise.ProductApplication.Products;
using SlothEnterprise.ProductApplication.Products.BusinessLoan;
using SlothEnterprise.ProductApplication.Tests.Data.Builders;
using Xunit;

namespace SlothEnterprise.ProductApplication.Tests.Products.BusinessLoans
{
    public class BusinessLoansServiceTests
    {
        private readonly Fixture fixture = new Fixture();

        [Theory]
        [InlineAutoData(1, true)]
        [InlineAutoData(50, true)]
        public async Task ProductApplicationService_SubmitApplicationFor_WhenCalledWithBusinessLoans_ShouldReturnApplicationId(
            int applicationId,
            bool success)
        {
            var applicationResult = await this.RunBusinessLoanTest(
                applicationId,
                success,
                this.CreateBusinessLoansCommand());

            applicationResult.Should().BeEquivalentTo(new
            {
                Data = applicationId,
                Successful = true,
            });
        }

        [Theory]
        [InlineAutoData(1, false)]
        [InlineAutoData(50, false)]
        [InlineAutoData(null, true)]
        public async Task ProductApplicationService_SubmitApplicationFor_WhenCalledWithBusinessLoans_ShouldReturnMinusOne(
            int? applicationId,
            bool success)
        {
            var applicationResult = await this.RunBusinessLoanTest(
                applicationId,
                success,
                this.CreateBusinessLoansCommand());

            applicationResult.Should().BeEquivalentTo(
                new
                {
                    Successful = false,
                });
        }

        private async Task<IResult> RunBusinessLoanTest(
            int? applicationId,
            bool success,
            SubmitBusinessLoansCommand command)
        {
            var result = new ApplicationResultBuilder()
                .WithApplicationId(applicationId)
                .WithSuccess(success)
                .Build();

            var mock = new Mock<IBusinessLoansService>();
            mock
                .Setup(
                    m => m.SubmitApplicationFor(
                        It.IsAny<CompanyDataRequest>(),
                        It.IsAny<LoansRequest>()))
                .Returns(result);

            return await new BusinessLoansService(mock.Object)
                .ExecuteAsync(command);
        }

        private SubmitBusinessLoansCommand CreateBusinessLoansCommand()
        {
            return new SubmitBusinessLoansCommand(
                new SellerCompanyData
                {
                    DirectorName = this.fixture.Create<string>(),
                    Founded = this.fixture.Create<DateTime>(),
                    Name = this.fixture.Create<string>(),
                    Number = this.fixture.Create<int>(),
                },
                this.fixture.Create<decimal>(),
                this.fixture.Create<decimal>());
        }
    }
}