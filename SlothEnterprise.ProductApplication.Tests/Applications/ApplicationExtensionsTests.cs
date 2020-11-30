using System;
using FluentAssertions;
using Moq;
using SlothEnterprise.ProductApplication.Applications;
using SlothEnterprise.ProductApplication.Products;
using SlothEnterprise.ProductApplication.Products.BusinessLoan;
using SlothEnterprise.ProductApplication.Products.ConfidentialInvoiceDiscounts;
using SlothEnterprise.ProductApplication.Products.SelectiveInvoiceDiscounts;
using SlothEnterprise.ProductApplication.Tests.Data.Builders;
using Xunit;

namespace SlothEnterprise.ProductApplication.Tests.Applications
{
    public class ApplicationExtensionsTests
    {
        [Fact]
        public void ApplicationExtensions_ToSubmitCommand_WhenCalledWithBusinessLoans_ShouldReturnSubmitBusinessLoansCommand()
        {
            var application = new ApplicationBuilder()
                .WithProduct(new BusinessLoans())
                .Build();

            var command = application.ToSubmitCommand();

            Assert.IsType<SubmitBusinessLoansCommand>(command);
        }

        [Fact]
        public void ApplicationExtensions_ToSubmitCommand_WhenCalledWithSelectiveInvoiceDiscount_ShouldReturnSubmitSelectiveInvoiceDiscountCommand()
        {
            var application = new ApplicationBuilder()
                .WithProduct(new SelectiveInvoiceDiscount())
                .Build();

            var command = application.ToSubmitCommand();

            Assert.IsType<SubmitSelectiveInvoiceDiscountApplicationCommand>(command);
        }

        [Fact]
        public void ApplicationExtensions_ToSubmitCommand_WhenCalledWithConfidentialInvoiceDiscount_ShouldReturnSubmitConfidentialInvoiceDiscountCommand()
        {
            var application = new ApplicationBuilder()
                .WithProduct(new ConfidentialInvoiceDiscount())
                .Build();

            var command = application.ToSubmitCommand();

            Assert.IsType<SubmitConfidentialInvoiceDiscountCommand>(command);
        }

        [Fact]
        public void ApplicationExtensions_ToSubmitCommand_WhenCalledWithUnknownProduct_ShouldThrowException()
        {
            var application = new ApplicationBuilder()
                .WithProduct(new Mock<IProduct>().Object)
                .Build();

            Action action = () => application.ToSubmitCommand();

            action.Should().Throw<InvalidOperationException>();
        }
    }
}