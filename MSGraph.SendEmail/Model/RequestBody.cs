namespace MSGraph.SendEmail.Model
{
    public class RequestBody
    {
        public string Subject { get; set; }
        public string recipient { get; set; }
        public List<IFormFile> fileAttachment { get; set; }
        public string emailBody { get; set; }
    }
}
