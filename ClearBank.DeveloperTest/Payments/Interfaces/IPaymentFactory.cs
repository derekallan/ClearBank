using System;
using ClearBank.DeveloperTest.Types;

namespace ClearBank.DeveloperTest.Payments.Interfaces;

public interface IPaymentFactory
{
    IPaymentMethod Create(PaymentScheme paymentType);
}
