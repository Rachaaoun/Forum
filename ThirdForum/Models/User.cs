using System.ComponentModel.DataAnnotations;

namespace ThirdForum.Models
{
    public class User
    {
        [Key]
        public int Id { get; set; }
        [StringLength(255)]
        public string psuedo { get; set; }
        [StringLength(255)]
        public string avatar { get; set; }
        [StringLength(255)]
        public string email { get; set; }
        [StringLength(255)]
        public string motpasse { get; set; }
        [StringLength(255)]
        public string role { get; set; }
    }
}
