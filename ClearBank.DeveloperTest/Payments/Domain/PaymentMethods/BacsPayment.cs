using System;
using ClearBank.DeveloperTest.Accounts.Domain;
using ClearBank.DeveloperTest.Payments.Interfaces;
using ClearBank.DeveloperTest.Types;

namespace ClearBank.DeveloperTest.Payments.Domain.PaymentMethods;

public class BacsPayment : IPaymentMethod
{
    public void ApplyPaymentToAccount(MakePaymentRequest request, Account account)
    {
        account.Balance -= request.Amount;
    }

    public MakePaymentResult ValidateRequestForAccount(MakePaymentRequest request, Account account)
    {
        if (!account.AllowedPaymentSchemes.HasFlag(AllowedPaymentSchemes.Bacs))
        {
            return MakePaymentResult.Fail();
        }
        return MakePaymentResult.Ok();
    }
}
