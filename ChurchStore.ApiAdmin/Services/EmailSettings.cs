namespace ChurchStore.ApiAdmin.Services
{
    public class EmailSettings
    {
        public string EmailNome { get; set; }
        public string EmailRemetente { get; set; }
        public string Senha { get; set; }
        public string SmtpServer { get; set; }
        public int Porta { get; set; }
    }
}
