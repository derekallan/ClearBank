using System;
using ClearBank.DeveloperTest.Accounts.Domain;
using ClearBank.DeveloperTest.Payments.Interfaces;
using ClearBank.DeveloperTest.Types;

namespace ClearBank.DeveloperTest.Payments.Domain.PaymentMethods;

public class ChapsPayment : IPaymentMethod
{
    public PaymentScheme SupportedScheme => PaymentScheme.Chaps;

    public void ApplyPaymentToAccount(MakePaymentRequest request, Account account)
    {
        account.Balance -= request.Amount;
    }
    public MakePaymentResult ValidateRequestForAccount(MakePaymentRequest request, Account account)
    {
        if (!account.AllowedPaymentSchemes.HasFlag(AllowedPaymentSchemes.Chaps))
        {
            return MakePaymentResult.Fail();
        }
        else if (account.Status != AccountStatus.Live)
        {
            return MakePaymentResult.Fail();
        }
        return MakePaymentResult.Ok();
    }
}
