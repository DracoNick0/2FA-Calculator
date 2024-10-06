namespace _2FA_Calculator.ServerSide
{
    class OTPGenerator
    {
        public string generateOTP()
        {
            string OTP = string.Empty;

            Random rand = new Random();
            for (int i = 0; i < 6; i++)
            {
                OTP += rand.Next(0, 9).ToString();
            }

            return OTP;
        }
    }
}
