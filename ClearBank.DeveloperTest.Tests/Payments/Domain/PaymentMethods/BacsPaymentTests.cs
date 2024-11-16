using System;
using ClearBank.DeveloperTest.Accounts.Domain;
using ClearBank.DeveloperTest.Payments.Domain.PaymentMethods;
using ClearBank.DeveloperTest.Types;
using Xunit;

namespace ClearBank.DeveloperTest.Tests.Payments.Domain.PaymentMethods;

public class BacsPaymentTests
{
    private readonly BacsPayment _sut;

    public BacsPaymentTests()
    {
        _sut = new BacsPayment();
    }

    [Fact]
    public void ApplyPaymentToAccount_ShouldSubtractAmountFromAccountBalance()
    {
        var account = new Account
        {
            Balance = 100
        };
        var request = new MakePaymentRequest
        {
            Amount = 50
        };

        _sut.ApplyPaymentToAccount(request, account);

        Assert.Equal(50, account.Balance);
    }

    [Fact]
    public void ValidateRequestForAccount_ShouldReturnFail_WhenAccountDoesNotAllowBacsPayments()
    {
        var account = new Account
        {
            AllowedPaymentSchemes = AllowedPaymentSchemes.FasterPayments | AllowedPaymentSchemes.Chaps,
            Balance = 100
        };
        var request = new MakePaymentRequest
        {
            Amount = 50
        };

        var result = _sut.ValidateRequestForAccount(request, account);

        Assert.False(result.Success);
    }

    [Theory]
    [InlineData(50, 100)]
    [InlineData(100, 100)]
    [InlineData(100, 150)]
    public void ValidateRequestForAccount_ShouldReturnOk_WhenAnyRequestAmountGiven(decimal balance, decimal amount)
    {
        var account = new Account
        {
            AllowedPaymentSchemes = AllowedPaymentSchemes.Bacs,
            Balance = balance
        };
        var request = new MakePaymentRequest
        {
            Amount = amount
        };

        var result = _sut.ValidateRequestForAccount(request, account);

        Assert.True(result.Success);
    }
}
