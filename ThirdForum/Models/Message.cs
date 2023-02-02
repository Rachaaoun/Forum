using System.ComponentModel.DataAnnotations;

namespace ThirdForum.Models
{
    public class Message
    {
        [Key]
        public int Id { get; set; }

        public string contenu { get; set; }
        public DateTime CreatedAt { get; set; }
     
    }
}
