namespace REACT_API.Utils
{
    public class EmailSettings
    {
        public string FromEmailID { get; set; }
        public string SMTPHost { get; set; }
        public int SMTPPort { get; set; }
        public string UserEmailID { get; set; }
        public string UserEmailPassword { get; set; }
        public bool EnableSSL { get; set; }
    }
}
