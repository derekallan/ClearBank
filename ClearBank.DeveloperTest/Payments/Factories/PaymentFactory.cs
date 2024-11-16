using System;
using ClearBank.DeveloperTest.Payments.Domain.PaymentMethods;
using ClearBank.DeveloperTest.Payments.Interfaces;
using ClearBank.DeveloperTest.Types;

namespace ClearBank.DeveloperTest.Payments.Factories;

public class PaymentFactory : IPaymentFactory
{
    public IPaymentMethod Create(PaymentScheme paymentType)
    {
        return paymentType switch
        {
            PaymentScheme.Bacs => new BacsPayment(),
            PaymentScheme.Chaps => new ChapsPayment(),
            PaymentScheme.FasterPayments => new FasterPayment(),
            _ => throw new ArgumentException("Invalid payment type"),
        };
    }
}
