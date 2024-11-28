using System;
using System.Collections.Generic;
using System.Linq;
using ClearBank.DeveloperTest.Payments.Interfaces;
using ClearBank.DeveloperTest.Types;

namespace ClearBank.DeveloperTest.Payments.Factories;

public class PaymentFactory : IPaymentFactory
{
    private Dictionary<PaymentScheme, IPaymentMethod> _paymentMethods;

    public PaymentFactory(IEnumerable<IPaymentMethod> paymentMethods)
    {
        _paymentMethods = paymentMethods.ToDictionary(x => x.SupportedScheme);
    }
    public IPaymentMethod Create(PaymentScheme paymentType)
    {
        if (_paymentMethods.ContainsKey(paymentType))
        {
            return _paymentMethods[paymentType];
        }
        throw new ArgumentException($"Payment type {paymentType} is not supported");
    }
}
