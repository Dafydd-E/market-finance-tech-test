using System;
using AutoFixture.Xunit2;
using FluentAssertions;
using Moq;
using SlothEnterprise.ProductApplication.Applications;
using SlothEnterprise.ProductApplication.Products;
using SlothEnterprise.ProductApplication.Tests.Data.Builders;
using Xunit;

namespace SlothEnterprise.ProductApplication.Tests
{
    public class ProductApplicationTests
    {
        [Theory]
        [InlineAutoData(10)]
        [InlineAutoData(-1)]
        public void ProductApplicationService_SubmitApplicationFor_WhenCalledWithSelectiveInvoiceDiscountProduct_ShouldReturnApplicationId(
            int applicationId)
        {
            var productApplicationService = new ProductApplicationServiceBuilder()
                .WithSelectInvoiceResult(applicationId)
                .Build();

            var application = new ApplicationBuilder()
                .WithProduct(new SelectiveInvoiceDiscount())
                .WithSellerCompanyData(new SellerCompanyData())
                .Build();

            var result = productApplicationService.SubmitApplicationFor(application);
            result.Should().Be(applicationId);
        }

        [Theory]
        [AutoData]
        public void ProductApplicationService_SubmitApplicationFor_WhenCalledWithSelectiveInvoiceDiscountProduct_ShouldResolveExceptionToErrorCode(
            Exception exception)
        {
            var productApplicationService = new ProductApplicationServiceBuilder()
                .WithSelectInvoiceException(exception)
                .Build();

            var application = new ApplicationBuilder()
                .WithProduct(new SelectiveInvoiceDiscount())
                .WithSellerCompanyData(new SellerCompanyData())
                .Build();

            var result = productApplicationService.SubmitApplicationFor(application);
            result.Should().Be(-1);
        }

        [Theory]
        [InlineAutoData(10, true, 10)]
        [InlineAutoData(10, false, -1)]
        [InlineAutoData(null, true, -1)]
        public void ProductApplicationService_SubmitApplicationFor_WhenCalledWithConfidentialInvoiceDiscountsProduct_ShouldReturnResultDataOrErrorCode(
            int? applicationId,
            bool success,
            int expected)
        {
            var applicationResult = new ApplicationResultBuilder()
                .WithApplicationId(applicationId)
                .WithSuccess(success)
                .Build();

            var productApplicationService = new ProductApplicationServiceBuilder()
                .WithConfidentialInvoiceResult(applicationResult)
                .Build();

            var application = new ApplicationBuilder()
                .WithProduct(new ConfidentialInvoiceDiscount())
                .WithSellerCompanyData(new SellerCompanyData())
                .Build();

            var result = productApplicationService.SubmitApplicationFor(application);
            result.Should().Be(expected);
        }

        [Theory]
        [InlineAutoData(10, true, 10)]
        [InlineAutoData(10, false, -1)]
        [InlineAutoData(null, true, -1)]
        public void ProductApplicationService_SubmitApplicationFor_WhenCalledWithBusinessLoansProduct_ShouldReturnResultDataOrErrorCode(
            int? applicationId,
            bool success,
            int expected)
        {
            var applicationResult = new ApplicationResultBuilder()
                .WithApplicationId(applicationId)
                .WithSuccess(success)
                .Build();

            var productApplicationService = new ProductApplicationServiceBuilder()
                .WithBusinessLoansResult(applicationResult)
                .Build();

            var application = new ApplicationBuilder()
                .WithProduct(new BusinessLoans())
                .WithSellerCompanyData(new SellerCompanyData())
                .Build();

            var result = productApplicationService.SubmitApplicationFor(application);

            result.Should().Be(expected);
        }
    }
}