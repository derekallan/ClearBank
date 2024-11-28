using System;
using ClearBank.DeveloperTest.Accounts.Domain;
using ClearBank.DeveloperTest.Payments.Interfaces;
using ClearBank.DeveloperTest.Types;

namespace ClearBank.DeveloperTest.Payments.Domain.PaymentMethods;

public class FasterPayment : IPaymentMethod
{
    public PaymentScheme SupportedScheme => PaymentScheme.FasterPayments;

    public void ApplyPaymentToAccount(MakePaymentRequest request, Account account)
    {
        account.Balance -= request.Amount;
    }

    public MakePaymentResult ValidateRequestForAccount(MakePaymentRequest request, Account account)
    {
        if (!account.AllowedPaymentSchemes.HasFlag(AllowedPaymentSchemes.FasterPayments))
        {
            return MakePaymentResult.Fail();
        }
        else if (account.Balance < request.Amount)
        {
            return MakePaymentResult.Fail();
        }
        return MakePaymentResult.Ok();
    }
}
