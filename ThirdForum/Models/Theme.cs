using System.ComponentModel.DataAnnotations;

namespace ThirdForum.Models
{
    public class Theme
    {

        [Key]
        public int Id { get; set; }
        public string NomTheme { get; set; }

        public List<Sujet> Sujets { get; set; }
    }
}
