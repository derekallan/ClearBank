using System;
using ClearBank.DeveloperTest.Accounts.Domain;
using ClearBank.DeveloperTest.Payments.Domain.PaymentMethods;
using ClearBank.DeveloperTest.Types;
using Xunit;

namespace ClearBank.DeveloperTest.Tests.Payments.Domain.PaymentMethods;

public class FasterPaymentTests
{
    private readonly FasterPayment _sut;

    public FasterPaymentTests()
    {
        _sut = new FasterPayment();
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
    public void ValidateRequestForAccount_ShouldReturnFail_WhenAccountDoesNotAllowFasterPayments()
    {
        var account = new Account
        {
            AllowedPaymentSchemes = AllowedPaymentSchemes.Bacs | AllowedPaymentSchemes.Chaps,
            Balance = 100
        };
        var request = new MakePaymentRequest
        {
            Amount = 50
        };

        var result = _sut.ValidateRequestForAccount(request, account);

        Assert.False(result.Success);
    }

    [Fact]
    public void ValidateRequestForAccount_ShouldReturnFail_WhenAccountBalanceIsLessThanRequestedAmount()
    {
        var account = new Account
        {
            AllowedPaymentSchemes = AllowedPaymentSchemes.FasterPayments,
            Balance = 100
        };
        var request = new MakePaymentRequest
        {
            Amount = 150
        };

        var result = _sut.ValidateRequestForAccount(request, account);

        Assert.False(result.Success);
    }

    [Theory]
    [InlineData(100, 50)]
    [InlineData(100, 100)]
    public void ValidateRequestForAccount_ShouldReturnOk_WhenAccountAllowsFasterPaymentsAndBalanceIsGreaterThanOrEqualToRequestedAmount(
        decimal balance, decimal amount)
    {
        var account = new Account
        {
            AllowedPaymentSchemes = AllowedPaymentSchemes.FasterPayments,
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
