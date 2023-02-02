namespace ThirdForum.Models
{
    public class MessageSujet
    {
        public Sujet sujet { get; set; }
        public IEnumerable<MessageSujetUser> messageSujetUsers { get; set; }
        public int userid { get; set; }
    }
}
