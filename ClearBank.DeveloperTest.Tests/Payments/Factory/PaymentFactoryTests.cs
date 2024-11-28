using System;
using System.Collections.Generic;
using ClearBank.DeveloperTest.Payments.Domain.PaymentMethods;
using ClearBank.DeveloperTest.Payments.Factories;
using ClearBank.DeveloperTest.Payments.Interfaces;
using ClearBank.DeveloperTest.Types;
using Xunit;

namespace ClearBank.DeveloperTest.Tests.Payments.Factory;

public class PaymentFactoryTests
{
    private readonly PaymentFactory _sut;

    public PaymentFactoryTests()
    {
        _sut = new PaymentFactory(new List<IPaymentMethod>
        {
            new BacsPayment(),
            new ChapsPayment(),
            new FasterPayment()
        });
    }

    [Fact]
    public void Create_ShouldReturnPaymentMethod_ForAllEnumValues()
    {
        foreach (var value in Enum.GetValues(typeof(PaymentScheme)))
        {
            var result = _sut.Create((PaymentScheme)value);

            Assert.NotNull(result);
        }
    }

    [Theory]
    [InlineData(PaymentScheme.Bacs, typeof(BacsPayment))]
    [InlineData(PaymentScheme.Chaps, typeof(ChapsPayment))]
    [InlineData(PaymentScheme.FasterPayments, typeof(FasterPayment))]
    public void Create_ShouldReturnCorrectPaymentMethod_ForPaymentScheme(PaymentScheme paymentScheme, Type expectedType)
    {
        var result = _sut.Create(paymentScheme);

        Assert.IsType(expectedType, result);
    }
}
