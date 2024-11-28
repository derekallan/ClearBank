using ClearBank.DeveloperTest.Accounts.Domain;
using ClearBank.DeveloperTest.Types;

namespace ClearBank.DeveloperTest.Payments.Interfaces;

public interface IPaymentMethod
{
    public PaymentScheme SupportedScheme { get; }
    public MakePaymentResult ValidateRequestForAccount(MakePaymentRequest request, Account account);
    public void ApplyPaymentToAccount(MakePaymentRequest request, Account account);
}
