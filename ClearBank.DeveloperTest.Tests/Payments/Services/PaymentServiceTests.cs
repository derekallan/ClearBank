using System;
using ClearBank.DeveloperTest.Accounts.Domain;
using ClearBank.DeveloperTest.Accounts.Interfaces;
using ClearBank.DeveloperTest.Payments.Interfaces;
using ClearBank.DeveloperTest.Services;
using ClearBank.DeveloperTest.Types;
using Moq;
using Xunit;

namespace ClearBank.DeveloperTest.Tests.Payments.Services;

public class PaymentServiceTests
{
    private readonly PaymentService _sut;
    private readonly Mock<IAccountDataStore> _accountDataStoreMock = new();
    private readonly Mock<IAccountDataStoreFactory> _accountDataStoreFactory = new();
    private readonly Mock<IPaymentFactory> _paymentFactoryMock = new();
    private readonly Mock<IPaymentMethod> _paymentMethodMock = new();
    public PaymentServiceTests()
    {
        _sut = new PaymentService(_accountDataStoreFactory.Object, _paymentFactoryMock.Object);
    }

    [Fact]
    public void MakePayment_ShouldReturnFailResult_WhenAccountIsNull()
    {
        SetupAccountDataStoreFactory();
        _accountDataStoreMock.Setup(x => x.GetAccount(It.IsAny<string>())).Returns((Account?)null);
        var request = new MakePaymentRequest();
        var result = _sut.MakePayment(request);
        Assert.False(result.Success);
    }

    [Fact]
    public void MakePayment_ShouldReturnFailResult_WhenValidationResultIsNotSuccessful()
    {
        SetupAccountDataStoreFactory();
        _accountDataStoreMock.Setup(x => x.GetAccount(It.IsAny<string>())).Returns(new Account());
        SetupPaymentFactory();
        _paymentMethodMock.Setup(x => x.ValidateRequestForAccount(It.IsAny<MakePaymentRequest>(), It.IsAny<Account>())).Returns(MakePaymentResult.Fail());
        var request = new MakePaymentRequest();
        var result = _sut.MakePayment(request);
        Assert.False(result.Success);
    }

    [Fact]
    public void MakePayment_ShouldUpdateAccountAndApplyPayment_WhenValidationResultIsSuccessful()
    {
        SetupAccountDataStoreFactory();
        var account = new Account();
        _accountDataStoreMock.Setup(x => x.GetAccount(It.IsAny<string>())).Returns(account);
        SetupPaymentFactory();
        _paymentMethodMock.Setup(x => x.ValidateRequestForAccount(It.IsAny<MakePaymentRequest>(), It.IsAny<Account>())).Returns(MakePaymentResult.Ok());
        var request = new MakePaymentRequest();
        _sut.MakePayment(request);
        _paymentMethodMock.Verify(x => x.ApplyPaymentToAccount(request, account), Times.Once);
        _accountDataStoreMock.Verify(x => x.UpdateAccount(account), Times.Once);
    }

    [Fact]
    public void MakePayment_ShouldReturnSuccess_WhenValidationResultIsSuccessful()
    {
        SetupAccountDataStoreFactory();
        _accountDataStoreMock.Setup(x => x.GetAccount(It.IsAny<string>())).Returns(new Account());
        SetupPaymentFactory();
        _paymentMethodMock.Setup(x => x.ValidateRequestForAccount(It.IsAny<MakePaymentRequest>(), It.IsAny<Account>())).Returns(MakePaymentResult.Ok());
        var request = new MakePaymentRequest();
        var result = _sut.MakePayment(request);
        Assert.True(result.Success);
    }
    private void SetupAccountDataStoreFactory()
    {
        _accountDataStoreFactory.Setup(x => x.Create()).Returns(_accountDataStoreMock.Object);
    }
    private void SetupPaymentFactory()
    {
        _paymentFactoryMock.Setup(x => x.Create(It.IsAny<PaymentScheme>())).Returns(_paymentMethodMock.Object);
    }
}
