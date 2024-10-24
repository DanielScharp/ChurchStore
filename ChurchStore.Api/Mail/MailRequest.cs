namespace ChurchStore.Api.Mail
{
    public class MailRequest
    {
        public string MailTo { get; set; }
        public string ReplayTo { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }

        public List<IFormFile> Attachments { get; set; }
    }
}
