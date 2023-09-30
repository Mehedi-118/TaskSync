using System;
using System.Threading.Tasks;

using Microsoft.Extensions.Options;

using MimeKit;
using MimeKit.Text;
using MailKit.Net.Smtp;
using MailKit.Security;
using PTSL.Ovidhan.Common.Helper;
using PTSL.Ovidhan.Common.Model.EntityViewModels.SystemUser;
using PTSL.Ovidhan.Service.Services.Interface.SystemUser;
using Microsoft.Extensions.Configuration;

namespace PTSL.Ovidhan.Service.Services.Implementation.SystemUser
{
    public class MessageService : IMessageService
    {
        private readonly MailSettings _mailSettings;
        private readonly IConfiguration _configuration;
        public MessageService(IOptions<MailSettings> mailSettings, IConfiguration configuration)
        {
            _mailSettings = mailSettings.Value;
            _configuration = configuration;
        }
        public async Task SendEmailAsync(MailRequest mailRequest)
        {
            try
            {
                string? emailTemplate= _configuration.GetSection("OTP:EmailBody").Value;
                string emailBody = emailTemplate.Replace("{otp}", mailRequest.OTP);
                var email = new MimeMessage();
                email.From.Add(MailboxAddress.Parse(_mailSettings.Mail));
                email.To.Add(MailboxAddress.Parse(mailRequest.ToEmail));
                email.Subject = _configuration.GetSection("OTP:EmailSubject").Value;
                email.Body = new TextPart(TextFormat.Html) { Text = emailBody };
                using var smtp = new SmtpClient();
                smtp.Connect(_mailSettings.Host, _mailSettings.Port, SecureSocketOptions.StartTls);
                smtp.Authenticate(_mailSettings.Mail, _mailSettings.Password);
                smtp.Send(email);
                smtp.Disconnect(true);
            }
            catch (Exception e)
            {

                throw;
            }

        }
        public void SendMessage()
        {
            throw new NotImplementedException();
        }
    }
}
