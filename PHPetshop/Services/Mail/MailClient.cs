using MailKit;
using MailKit.Net.Smtp;
using MimeKit;
using PHPetshop.Models;

namespace PHPetshop.Services.Mail {
    public class MailClient {
        private SmtpClient client = new SmtpClient();
        private MailClientConfiguration Config { get; }

        public MailClient(MailClientConfiguration config) {
            Config = config;
        }

        public MimeMessage CreateMessage(string adresseName, string adresseMail, string subject, string content) {
            var message = new MimeMessage();
            var bodyBuilder = new BodyBuilder();

            bodyBuilder.HtmlBody = content;

            message.From.Add(new MailboxAddress("PH PetShop", Config.Login));
            message.To.Add(new MailboxAddress(adresseName, adresseMail));
            message.Subject = subject;
            message.Body = bodyBuilder.ToMessageBody();

            return message;
        }

        public async Task SendAsync(MimeMessage message) {
            await client.ConnectAsync(Config.Server, Config.Port);

            await client.AuthenticateAsync(Config.Login, Config.Password);

            await client.SendAsync(message);

            await client.DisconnectAsync(true);
        }
    }
}
