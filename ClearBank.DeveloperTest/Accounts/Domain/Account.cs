namespace ClearBank.DeveloperTest.Accounts.Domain
{
    public class Account
    {
        public string AccountNumber { get; set; } = string.Empty;
        public decimal Balance { get; set; }
        public AccountStatus Status { get; set; }
        public AllowedPaymentSchemes AllowedPaymentSchemes { get; set; }
    }
}
