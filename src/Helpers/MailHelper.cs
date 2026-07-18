using MailKit.Net.Smtp;
using MimeKit;

namespace to_do_list.src.Helpers
{
    public class MailHelper
    {
        private readonly string EmailFrom = Environment.GetEnvironmentVariable("EMAIL_FROM") ?? "";
        private readonly string Password = Environment.GetEnvironmentVariable("PASSWORD_EMAIL") ?? "";
        public async Task<bool> SendMail(string recipient, string subject, string body)
        {
            try
            {
                MimeMessage message = new();
                message.From.Add(MailboxAddress.Parse(EmailFrom));
                message.To.Add(MailboxAddress.Parse(recipient));
                message.Subject = subject;
                message.Body = new TextPart("html")
                {
                    Text = body  
                };

                using SmtpClient smtp = new();
                await smtp.ConnectAsync("smtp.gmail.com", 587, MailKit.Security.SecureSocketOptions.StartTls);
                await smtp.AuthenticateAsync(EmailFrom, Password);
                await smtp.SendAsync(message);
                await smtp.DisconnectAsync(true);
                
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}