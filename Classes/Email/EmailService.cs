using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Azure;
using Azure.Communication.Email;

namespace StrategySync.Classes.Email
{
    public class EmailService
    {
        private const string ConnectionString = "endpoint=https://strategysyncconnection.unitedstates.communication.azure.com/;accesskey=8FBTh7z1NGNlU70aeeCYVfvJLBNi9qvdOuO01BueR45m3enHMjctJQQJ99ALACULyCpKfgdmAAAAAZCSQ1RY";

        private readonly string _fromEmailAddress = "DoNotReply@9aa20181-3b72-46e8-b0ab-83466eab8a6f.azurecomm.net";

        public async Task<bool> SendEmailAsync(string toEmail, string subject, string body)
        {

            var emailClient = new EmailClient(ConnectionString);

            var emailContent = new EmailContent(subject)
            {
                PlainText = body
            };

            var emailRecipients = new EmailRecipients(new List<EmailAddress>
            {
                new EmailAddress(toEmail) 
            });

            var emailMessage = new EmailMessage(_fromEmailAddress, emailRecipients, emailContent);

            var sendOperation = await emailClient.SendAsync(WaitUntil.Started, emailMessage);

            var sendResult = await sendOperation.WaitForCompletionAsync();

            return sendResult.Value.Status == EmailSendStatus.Succeeded;
        }
    }
}
