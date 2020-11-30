using System;
using AutoFixture.Xunit2;
using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using SlothEnterprise.External.V1;
using SlothEnterprise.ProductApplication.Products;
using SlothEnterprise.ProductApplication.Products.SelectiveInvoiceDiscounts;
using Xunit;

namespace SlothEnterprise.ProductApplication.Tests.Products
{
    public class ProductServiceFactoryTests
    {
        [Theory]
        [AutoData]
        public void ProductServiceFactory_GetHandlersForCommand_WhenCalledWithSelectiveInvoiceCommand_ShouldReturnDiscountService(
            SubmitSelectiveInvoiceDiscountApplicationCommand command,
            Mock<ISelectInvoiceService> selectInvoiceService)
        {
            var collection = new ServiceCollection();
            collection
                .AddSingleton(typeof(ISelectInvoiceService), selectInvoiceService.Object)
                .AddProductServices();

            var resolvedHandler = new ProductServiceFactory(collection.BuildServiceProvider())
                .GetHandlerForCommand(command);
            resolvedHandler.Should().BeOfType<SelectiveInvoiceDiscountService>();
        }

        [Theory]
        [AutoData]
        public void ProductServiceFactory_GetHandlersForCommand_WhenCalledWithInvalidCommand_ShouldThrowException(
            object unregisteredCommand,
            Mock<ISelectInvoiceService> selectInvoiceService)
        {
            var collection = new ServiceCollection();
            collection
                .AddSingleton(typeof(ISelectInvoiceService), selectInvoiceService.Object)
                .AddProductServices();

            Action action = () => new ProductServiceFactory(collection.BuildServiceProvider())
                .GetHandlerForCommand(unregisteredCommand);
            
            action.Should().Throw<InvalidOperationException>();
        }
    }
}