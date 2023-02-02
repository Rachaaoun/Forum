namespace ThirdForum.Models
{
    public class HomeModel
    {
        public User user { get; set; }
        public IEnumerable<Theme> themes { get; set; }
    }
}
