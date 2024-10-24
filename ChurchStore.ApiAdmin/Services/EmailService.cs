using ChurchStore.ApiAdmin.Mail;
using MimeKit;
using MailKit.Net.Smtp;
using Microsoft.Extensions.Options;

namespace ChurchStore.ApiAdmin.Services
{
    public class EmailService
    {
        private readonly EmailSettings _emailSettings;

        public EmailService(IOptions<EmailSettings> emailSettings)
        {
            _emailSettings = emailSettings.Value;
        }

        public async Task EnviarEmail(MailRequest props)
        {
            // instanciar classe de mensagem 'mimemessage' 
            var message = new MimeMessage();

            //from address
            message.From.Add(new MailboxAddress(_emailSettings.EmailNome, _emailSettings.EmailRemetente));

            // subject
            message.Subject = props.Subject;

            //to address
            message.To.Add(new MailboxAddress(props.ReplayTo, props.MailTo));

            //body
            message.Body = new TextPart("html")
            {
                Text = props.Body,
            };

            using (var client = new SmtpClient())
            {
                await client.ConnectAsync(_emailSettings.SmtpServer, _emailSettings.Porta, true);

                await client.AuthenticateAsync(_emailSettings.EmailRemetente, _emailSettings.Senha);

                await client.SendAsync(message);

                await client.DisconnectAsync(true);
            }
        }

    }
}
