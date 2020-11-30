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
using SlothEnterprise.ProductApplication.Products.ConfidentialInvoiceDiscounts;
using SlothEnterprise.ProductApplication.Tests.Data.Builders;
using Xunit;

namespace SlothEnterprise.ProductApplication.Tests.ConfidentialInvoiceDiscounts
{
    public class ConfidentialInvoiceDiscountServiceTests
    {
        private readonly Fixture fixture = new Fixture();
        
        [Theory]
        [InlineAutoData(1, true)]
        [InlineAutoData(50, true)]
        public async Task ProductApplicationService_SubmitApplicationFor_WhenCalledWithConfidentialInvoiceDiscount_ShouldReturnApplicationId(
            int applicationId,
            bool success)
        {
            var applicationResult = await this.RunConfidentialInvoiceDiscountTest(
                applicationId,
                success,
                this.CreateConfidentialInvoiceDiscountCommand());

            applicationResult.Should().BeEquivalentTo(
                new
                {
                    Successful = true,
                    Data = applicationId
                });
        }

        [Theory]
        [InlineAutoData(1, false)]
        [InlineAutoData(50, false)]
        [InlineAutoData(null, true)]
        public async Task ProductApplicationService_SubmitApplicationFor_WhenCalledWithConfidentialInvoiceDiscount_ShouldReturnMinusOne(
            int? applicationId,
            bool success)
        {
            var applicationResult = await this.RunConfidentialInvoiceDiscountTest(
                applicationId,
                success,
                this.CreateConfidentialInvoiceDiscountCommand());

            applicationResult.Should().BeEquivalentTo(
                new
                {
                    Successful = false,
                });
        }

        private async Task<IResult> RunConfidentialInvoiceDiscountTest(
            int? applicationId,
            bool success,
            SubmitConfidentialInvoiceDiscountCommand command)
        {
            var result = new ApplicationResultBuilder()
                .WithApplicationId(applicationId)
                .WithSuccess(success)
                .Build();

            var mock = new Mock<IConfidentialInvoiceService>();
            mock
                .Setup(
                    m => m.SubmitApplicationFor(
                        It.IsAny<CompanyDataRequest>(),
                        It.IsAny<decimal>(),
                        It.IsAny<decimal>(),
                        It.IsAny<decimal>()))
                .Returns(result);

            return await new ConfidentialInvoiceDiscountService(mock.Object)
                .ExecuteAsync(command);
        }

        private SubmitConfidentialInvoiceDiscountCommand CreateConfidentialInvoiceDiscountCommand()
        {
            return new SubmitConfidentialInvoiceDiscountCommand(
                this.fixture.Create<decimal>(),
                this.fixture.Create<decimal>(),
                this.fixture.Create<decimal>(),
                new SellerCompanyData
                {
                    DirectorName = this.fixture.Create<string>(),
                    Founded = this.fixture.Create<DateTime>(),
                    Name = this.fixture.Create<string>(),
                    Number = this.fixture.Create<int>(),
                }
            );
        }
    }
}