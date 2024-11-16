namespace ClearBank.DeveloperTest.Types
{
    public class MakePaymentResult
    {
        public static MakePaymentResult Ok()
        {
            return new MakePaymentResult { Success = true };
        }
        public static MakePaymentResult Fail()
        {
            return new MakePaymentResult { Success = false };
        }

        public bool Success { get; set; }
    }
}
