using ChurchStore.Api.Mail;
using MailKit.Net.Smtp;
using MimeKit;


namespace ChurchStore.Api.Services
{
    public class EmailService
    {
        public async Task EnviarEmail(MailRequest props)
        {
            string emailRemetente = "scharp.suporte@gmail.com";
            // instanciar classe de mensagem 'mimemessage' 
            var message = new MimeMessage();

            //from address
            message.From.Add(new MailboxAddress("Livres Church", emailRemetente));

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
                client.Connect("smtp.gmail.com", 465, true); //465 é a porta do seu servidor de email

                client.Authenticate(emailRemetente, "");

                client.Send(message);

                client.Disconnect(true);
            }
        }

    }
}
