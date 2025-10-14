namespace Theater_Management_BE.src.Infrastructure.Utils
{
    public static class OtpUtil
    {
        private static readonly Random _random = new Random();

        public static int GenerateOtp()
        {
            return _random.Next(0, 1_000_000);
        }
    }
}
