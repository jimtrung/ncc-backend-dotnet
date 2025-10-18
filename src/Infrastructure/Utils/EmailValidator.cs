using System.Net;
using System.Net.Mail;
using System.Text.RegularExpressions;
using DnsClient;

namespace Theater_Management_BE.src.Infrastructure.Utils
{
    public class EmailValidator
    {
        private readonly Regex _emailRegex = new(@"^[A-Za-z0-9._%+-]+@[A-Za-z0-9.-]+\.[A-Za-z]{2,}$");
        private readonly IConfiguration _config;
        private readonly SmtpClient _smtpClient;

        public EmailValidator(IConfiguration config)
        {
            _config = config;

            _smtpClient = new SmtpClient
            {
                Host = _config["Smtp:Host"],
                Port = int.Parse(_config["Smtp:Port"] ?? "587"),
                EnableSsl = true,
                Credentials = new NetworkCredential(
                    _config["Smtp:Username"],
                    _config["Smtp:Password"]
                )
            };
        }

        private bool IsValidSyntax(string email)
        {
            if (string.IsNullOrWhiteSpace(email)) return false;
            return _emailRegex.IsMatch(email);
        }

        private async Task<bool> HasMxRecordAsync(string domain)
        {
            try
            {
                var lookup = new LookupClient();
                var result = await lookup.QueryAsync(domain, QueryType.MX);
                return result.Answers.MxRecords().Count() > 0;
            }
            catch
            {
                return false;
            }
        }

        public async Task SendVerificationEmailAsync(string to, string verifyLink)
        {
            var from = _config["Smtp:From"] ?? "no-reply@yourapp.com";

            var message = new MailMessage(from, to)
            {
                Subject = "Verify your email",
                Body = GetEmailTemplate().Replace("{{verify_link}}", verifyLink),
                IsBodyHtml = true
            };

            // Optional debug restriction like your original code
            if (to.Equals("nguyenhaitrung737@gmail.com", StringComparison.OrdinalIgnoreCase))
                await _smtpClient.SendMailAsync(message);
        }

        public async Task<bool> IsValidEmailAsync(string email)
        {
            if (!IsValidSyntax(email))
                return false;

            var domain = email.Substring(email.IndexOf('@') + 1);
            return await HasMxRecordAsync(domain);
        }

        private string GetEmailTemplate()
        {
            return @"
<!DOCTYPE html>
<html>
<head>
  <meta charset='UTF-8'>
  <title>Verify your email</title>
  <style>
    body { font-family:'Segoe UI',Arial,sans-serif;background:#f4f6f8;color:#333; }
    .container { max-width:500px;margin:40px auto;background:#fff;border-radius:12px;box-shadow:0 4px 12px rgba(0,0,0,0.1); }
    .header { background:#0a66c2;color:white;text-align:center;padding:24px; }
    .header h1 { margin:0;font-size:24px; }
    .content { padding:24px; }
    .content p { font-size:15px;line-height:1.6;margin-bottom:20px; }
    .verify-btn { display:inline-block;padding:12px 24px;background:#0a66c2;color:white!important;text-decoration:none;border-radius:6px;font-weight:bold; }
    .footer { text-align:center;font-size:13px;color:#777;padding:16px;background:#f9fafb; }
  </style>
</head>
<body>
  <div class='container'>
    <div class='header'>
      <h1>Verify your email 🍌</h1>
    </div>
    <div class='content'>
      <p>Hi there! Thanks for signing up. Please confirm your email address by clicking the button below.</p>
      <p style='text-align:center;'>
        <a href='{{verify_link}}' class='verify-btn'>Verify Email</a>
      </p>
      <p>If you didn’t sign up for this account, you can safely ignore this message 🚬</p>
    </div>
    <div class='footer'>
      &copy; 2025 LinkedIn Clone · All rights reserved
    </div>
  </div>
</body>
</html>";
        }
    }
}
