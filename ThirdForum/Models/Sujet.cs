using System.ComponentModel.DataAnnotations;

namespace ThirdForum.Models
{
    public class Sujet
    {
        [Key]
        public int Id { get; set; }
        public string NomSujet { get; set; }
        public string Photo { get; set; }
        public string Description { get; set; }
        public  Theme Theme { get; set; }
            
    }
}
