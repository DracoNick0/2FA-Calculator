using System.Net.Mail;
using System.Net;

namespace _2FA_Calculator.ServerSide
{
    class Email2FA
    {
        OTPGenerator otpGenerator;
        UserManager userManager;
        private const string senderEmail = "ntstemporary7@gmail.com";
        private const string senderGoogleAppPassword = "qqud szzc jzdn mmai";
        private string? otp;

        public Email2FA()
        {
            this.otpGenerator = new OTPGenerator();
            this.userManager = new UserManager(@"../../../ServerSide/UserCredentialsStorage.txt");
            this.otp = null;
        }

        /**
         * Sends an email to the user's email, returns true if successfully sent, else false.
         */
        public bool sendOTPEmail(string userOrEmail)
        {
            using (MailMessage mail = new MailMessage())
            {
                mail.From = new MailAddress(Email2FA.senderEmail);

                if(RandomFunctions.isValidEmail(userOrEmail))
                {
                    mail.To.Add(new MailAddress(userOrEmail));
                }
                else
                {
                    mail.To.Add(new MailAddress(this.userManager.getUserEmail(userOrEmail)));
                }

                mail.Subject = "2FA-Calculator One Time Password";
                this.otp = this.otpGenerator.generateOTP();
                mail.Body = "Your OTP: " + this.otp;
                mail.IsBodyHtml = true;

                var smtp = new SmtpClient("smtp.gmail.com", 587)
                {
                    UseDefaultCredentials = false,
                    Credentials = new NetworkCredential(Email2FA.senderEmail, Email2FA.senderGoogleAppPassword),
                    EnableSsl = true,
                };

                smtp.Send(mail);
            }

            return true;
        }

        public bool authenticateOTP(string userInput)
        {
            if (this.otp != null && userInput != null)
            {
                // If the user input is the otp
                if (this.otp.CompareTo(userInput) == 0)
                {
                    return true;
                }
            }

            return false;
        }
    }
}
