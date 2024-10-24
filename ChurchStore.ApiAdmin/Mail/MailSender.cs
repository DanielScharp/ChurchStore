using ChurchStore.ApiAdmin.Services;
using ChurchStore.Domain;

namespace ChurchStore.ApiAdmin.Mail
{
    public class MailSender
    {
        private readonly EmailService _emailservice;

        public MailSender(EmailService emailservice)
        {
            _emailservice = emailservice;
        }


        //Envia email com dados de acesso solicitados pelo usuário
        public async Task EnviarEmailRecuperaSenha(Usuario cliente)
        {

            try
            {

                //Monta o email
                var strMsg = "<font face=tahoma, arial size=2>";
                strMsg += " Prezado " + cliente.Nome + ", <br/><br/>";
                strMsg += " Este e-mail tem como finalidade a notificação de sua nova senha de acesso ao site. <br/><br/>";
                strMsg += " Apelido/Login: " + cliente.Apelido + "<br/>";
                strMsg += " Senha: " + cliente.Senha + "<br/><br/>";
                strMsg += " <BR/><BR/><BR/>Atenciosamente, <br/> HelpDesk <BR/>";

                strMsg += " <font color=red>Não é preciso responder esta mensagem, pois trata-se de um email automático de nosso sistema.</font>";
                strMsg += " </font>";

                //Enviar a senha par ao e-mail do cliente
                var mailRequest = new MailRequest
                {
                    MailTo = cliente.Email,
                    Subject = "Novos dados de acesso",
                    Body = strMsg
                };


                //Envia o Email
                await _emailservice.EnviarEmail(mailRequest);
            }
            catch
            {
                throw;
            }

        }
    }
}
