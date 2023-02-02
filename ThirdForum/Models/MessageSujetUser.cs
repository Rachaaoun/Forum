using System.ComponentModel.DataAnnotations;

namespace ThirdForum.Models
{
    public class MessageSujetUser
    {
        [Key]
        public int Id { get; set; }
        public Sujet Sujets { get; set; }
        public User User { get; set; }
        public Message Message { get; set; }
    }
}
