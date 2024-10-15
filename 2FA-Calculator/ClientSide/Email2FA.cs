﻿using _2FA_Calculator.ServerSide;
using System.Net.Mail;
using System.Net;

namespace _2FA_Calculator.ClientSide
{
    class Email2FA
    {
        OTPGenerator otpGenerator;
        private string senderEmail;
        private string senderGoogleAppPassword;

        public Email2FA()
        {
            this.otpGenerator = new OTPGenerator();
            this.senderEmail = "ntstemporary7@gmail.com";
            this.senderGoogleAppPassword = "qqud szzc jzdn mmai";
        }

        /**
         * Sends an email to the user's email, returns true if successfully sent, else false.
         */
        public bool sendOTPEmail(string recieverEmail)
        {
            using (MailMessage mail = new MailMessage())
            {
                mail.From = new MailAddress(this.senderEmail);
                mail.To.Add(new MailAddress(recieverEmail));
                mail.Subject = "2FA-Calculator One Time Password";
                mail.Body = "Your OTP: " + this.otpGenerator.generateOTP();
                mail.IsBodyHtml = true;

                var smtp = new SmtpClient("smtp.gmail.com", 587)
                {
                    UseDefaultCredentials = false,
                    Credentials = new NetworkCredential(this.senderEmail, this.senderGoogleAppPassword),
                    EnableSsl = true,
                };

                smtp.Send(mail);
            }

            return true;
        }
    }
}
