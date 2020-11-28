using System;
using AutoFixture.Xunit2;
using FluentAssertions;
using Moq;
using SlothEnterprise.ProductApplication.Products;
using Xunit;

namespace SlothEnterprise.ProductApplication.Tests
{
    public class ProductApplicationTests
    {
        [Theory]
        [InlineAutoData(1, true)]
        [InlineAutoData(50, true)]
        public void ProductApplicationService_SubmitApplicationFor_WhenCalledWithConfidentialInvoiceDiscount_ShouldReturnApplicationId(
            int applicationId,
            bool success)
        {
            var applicationResult = this.RunConfidentialInvoiceDiscountTest(applicationId, success);
            applicationResult.Should().Be(applicationId);
        }

        [Theory]
        [InlineAutoData(1, false)]
        [InlineAutoData(50, false)]
        [InlineAutoData(null, true)]
        public void ProductApplicationService_SubmitApplicationFor_WhenCalledWithConfidentialInvoiceDiscount_ShouldReturnMinusOne(
            int? applicationId,
            bool success)
        {
            var applicationResult = this.RunConfidentialInvoiceDiscountTest(applicationId, success);
            applicationResult.Should().Be(-1);
        }

        [Theory]
        [InlineAutoData(1, true)]
        [InlineAutoData(50, true)]
        public void ProductApplicationService_SubmitApplicationFor_WhenCalledWithBusinessLoans_ShouldReturnApplicationId(
            int applicationId,
            bool success)
        {
            var applicationResult = this.RunBusinessLoanTest(applicationId, success);
            applicationResult.Should().Be(applicationId);
        }

        [Theory]
        [InlineAutoData(1, false)]
        [InlineAutoData(50, false)]
        [InlineAutoData(null, true)]
        public void ProductApplicationService_SubmitApplicationFor_WhenCalledWithBusinessLoans_ShouldReturnMinusOne(
            int? applicationId,
            bool success)
        {
            var applicationResult = this.RunBusinessLoanTest(applicationId, success);
            applicationResult.Should().Be(-1);
        }

        [Theory]
        [InlineAutoData(1)]
        [InlineAutoData(10)]
        public void ProductApplicationService_SubmitApplicationFor_WhenCalledWithSelectInvoiceDiscount_ShouldReturnServiceResult(
            int serviceResult)
        {
            var productApplicationService = new ProductApplicationServiceBuilder()
                .WithSelectInvoiceResult(serviceResult)
                .Build();

            var application = new ApplicationBuilder()
                .WithProduct(new SelectiveInvoiceDiscount())
                .Build();

            var applicationResult = productApplicationService
                .SubmitApplicationFor(application);
            applicationResult.Should().Be(serviceResult);
        }

        [Fact]
        public void ProductApplicationService_SubmitApplicationFor_WhenCalledWithoutRecognisedApplicationType_ShouldThrowException()
        {
            var productApplicationService = new ProductApplicationServiceBuilder().Build();
            var application = new ApplicationBuilder().WithProduct(new Mock<IProduct>().Object).Build();
            Action act = () => productApplicationService.SubmitApplicationFor(application);
            act.Should().Throw<InvalidOperationException>();
        }

        private int RunBusinessLoanTest(int? applicationId, bool success)
        {
            var result = new ApplicationResultBuilder()
                .WithApplicationId(applicationId)
                .WithSuccess(success)
                .Build();

            var productApplicationService = new ProductApplicationServiceBuilder()
                .WithBusinessLoansResult(result)
                .Build();

            var application = new ApplicationBuilder()
                .WithProduct(new BusinessLoans())
                .Build();

            return productApplicationService
                .SubmitApplicationFor(application);
        }

        private int RunConfidentialInvoiceDiscountTest(int? applicationId, bool success)
        {
            var result = new ApplicationResultBuilder()
                .WithApplicationId(applicationId)
                .WithSuccess(success)
                .Build();

            var productApplicationService = new ProductApplicationServiceBuilder()
                .WithConfidentialInvoiceResult(result)
                .Build();

            var application = new ApplicationBuilder()
                .WithProduct(new ConfidentialInvoiceDiscount())
                .Build();

            return productApplicationService
                .SubmitApplicationFor(application);
        }
    }
}