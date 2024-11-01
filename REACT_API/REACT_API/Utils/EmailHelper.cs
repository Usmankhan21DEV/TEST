using System.Net;
using System.Net.Mail;
using Microsoft.Extensions.Options;

namespace REACT_API.Utils
{
    public class EmailHelper
    {
        private readonly EmailSettings _emailSettings;

        // Constructor that takes the EmailSettings injected by the ASP.NET Core DI system
        public EmailHelper(IOptions<EmailSettings> emailSettings)
        {
            _emailSettings = emailSettings.Value;
        }

        // SendMail method
        public bool SendMail(string p_Subject, string p_Body, List<string> p_ToMailIds)
        {
            bool flag = false;
            try
            {
                // Enforce TLS 1.2 for security
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;

                // Prepare the mail message
                MailMessage mail = new MailMessage();
                foreach (string toMailId in p_ToMailIds)
                {
                    if (!string.IsNullOrEmpty(toMailId))
                    {
                        mail.To.Add(toMailId);
                    }
                }

                // Use email settings from appsettings.json
                mail.From = new MailAddress(_emailSettings.FromEmailID);
                mail.Subject = p_Subject;
                mail.Body = p_Body;
                mail.IsBodyHtml = true;

                // Add a custom Message-ID header
                string messageId = Guid.NewGuid().ToString();
                mail.Headers.Add("Message-ID", $"<{messageId}hrm@inventorysol.com>");

                // Configure the SMTP client
                SmtpClient smtp = new SmtpClient
                {
                    Host = _emailSettings.SMTPHost,
                    Port = _emailSettings.SMTPPort,
                    UseDefaultCredentials = false,
                    Credentials = new NetworkCredential(_emailSettings.UserEmailID, _emailSettings.UserEmailPassword),
                    EnableSsl = _emailSettings.EnableSSL
                };

                // Send the email
                smtp.Send(mail);
                flag = true;
            }
            catch (Exception)
            {
                flag = false;
            }

            return flag;
        }
    }

}
