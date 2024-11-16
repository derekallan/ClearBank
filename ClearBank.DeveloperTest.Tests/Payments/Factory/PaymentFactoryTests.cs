using System;
using ClearBank.DeveloperTest.Payments.Factories;
using ClearBank.DeveloperTest.Types;
using Xunit;

namespace ClearBank.DeveloperTest.Tests.Payments.Factory;

public class PaymentFactoryTests
{
    private readonly PaymentFactory _sut;

    public PaymentFactoryTests()
    {
        _sut = new PaymentFactory();
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
}
