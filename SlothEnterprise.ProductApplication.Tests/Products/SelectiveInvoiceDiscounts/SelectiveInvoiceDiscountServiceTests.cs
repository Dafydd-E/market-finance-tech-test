using System;
using System.Threading.Tasks;
using AutoFixture.Xunit2;
using FluentAssertions;
using Moq;
using SlothEnterprise.External.V1;
using SlothEnterprise.ProductApplication.Products;
using SlothEnterprise.ProductApplication.Products.SelectiveInvoiceDiscounts;
using Xunit;

namespace SlothEnterprise.ProductApplication.Tests.Products.SelectiveInvoiceDiscounts
{
    public class SelectiveInvoiceDiscountServiceTest
    {
        [Theory]
        [AutoData]
        public async Task SelectiveInvoiceDiscountService_ExecuteAsync_WhenCalledWithSelectInvoiceDiscount_ShouldReturnServiceResult(
            int serviceResult,
            decimal invoiceAmount,
            decimal advantagePercentage,
            int companyNumber)
        {
            var selectInvoiceServiceMock = new Mock<ISelectInvoiceService>();
            selectInvoiceServiceMock
                .Setup(
                    m => m.SubmitApplicationFor(
                        It.IsAny<string>(),
                        It.IsAny<decimal>(),
                        It.IsAny<decimal>()))
                .Returns(serviceResult);

            var applicationResult = await new SelectiveInvoiceDiscountService(selectInvoiceServiceMock.Object)
                .ExecuteAsync(new SubmitSelectiveInvoiceDiscountApplicationCommand(
                    invoiceAmount,
                    advantagePercentage,
                    companyNumber));
            applicationResult.Should().BeEquivalentTo(SubmitApplicationResult.Success(serviceResult));
        }

        [Theory]
        [AutoData]
        public async Task SelectiveInvoiceDiscountService_ExecuteAsync_WhenCalledWithSelectInvoiceDiscount_ShouldReturnFailedResult(
            decimal invoiceAmount,
            decimal advantagePercentage,
            int companyNumber,
            Exception exception)
        {
            var selectInvoiceServiceMock = new Mock<ISelectInvoiceService>();
            selectInvoiceServiceMock
                .Setup(
                    m => m.SubmitApplicationFor(
                        It.IsAny<string>(),
                        It.IsAny<decimal>(),
                        It.IsAny<decimal>()))
                .Throws(exception);

            var applicationResult = await new SelectiveInvoiceDiscountService(selectInvoiceServiceMock.Object)
                .ExecuteAsync(new SubmitSelectiveInvoiceDiscountApplicationCommand(
                    invoiceAmount,
                    advantagePercentage,
                    companyNumber));
            applicationResult.Should().BeEquivalentTo(SubmitApplicationResult.Failure(exception: exception));
        }
    }
}