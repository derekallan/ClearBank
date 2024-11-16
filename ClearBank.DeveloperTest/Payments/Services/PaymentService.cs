using ClearBank.DeveloperTest.Accounts.Interfaces;
using ClearBank.DeveloperTest.Payments.Interfaces;
using ClearBank.DeveloperTest.Types;

namespace ClearBank.DeveloperTest.Services
{
    public class PaymentService : IPaymentService
    {
        private readonly IAccountDataStoreFactory _accountDataStoreFactory;
        private readonly IPaymentFactory _paymentFactory;

        public PaymentService(
            IAccountDataStoreFactory accountDataStoreFactory,
            IPaymentFactory paymentFactory)
        {
            _accountDataStoreFactory = accountDataStoreFactory;
            _paymentFactory = paymentFactory;
        }

        public MakePaymentResult MakePayment(MakePaymentRequest request)
        {
            var accountDataStore = _accountDataStoreFactory.Create();

            var account = accountDataStore.GetAccount(request.DebtorAccountNumber);
            if (account is null)
            {
                return MakePaymentResult.Fail();
            }

            var payment = _paymentFactory.Create(request.PaymentScheme);
            var validationResult = payment.ValidateRequestForAccount(request, account);
            if (!validationResult.Success)
            {
                return validationResult;
            }

            payment.ApplyPaymentToAccount(request, account);

            accountDataStore.UpdateAccount(account);

            return validationResult;
        }
    }
}
