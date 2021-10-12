using System;
using System.Net.Mail;
using DailyVisitors.EnumConstants;
//using MailKit.Net.Smtp;
//using MailKit.Security;
using Microsoft.Extensions.Options;

namespace DailyVisitors.WebApi.Services
{
    public interface IEmailService
    {
        string Send(string to, string subject, string html);
    }

    public class EmailService:IEmailService
    {
        public string Send(string to, string subject, string html)
        {
            // create message
            var email = new MailMessage();
            email.From = new MailAddress(Settings.From);
            email.To.Add(to);
            email.Subject = subject;
            email.Body = html;
            email.IsBodyHtml = true;

            // send email
            try
            {
                using var smtp = new SmtpClient(Settings.SmtpHost);
                smtp.Port = Settings.SmtpPort;
                smtp.UseDefaultCredentials = false;
                smtp.EnableSsl = true;
                smtp.Credentials = new System.Net.NetworkCredential(Settings.SmtpUser, Settings.SmtpPass);
                smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
                smtp.Send(email);
                return "Success";
            }
            catch(Exception exception)
            {
                return "Error:" + exception.Message;
            }
            
        }
    }
}
