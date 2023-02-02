using Microsoft.EntityFrameworkCore;
using ThirdForum.Models;

namespace ThirdForum.Data
{
    public class DbContext_f:DbContext 
    {
        public DbContext_f(DbContextOptions<DbContext_f> options):base(options)
        {

        }
        public DbSet<Theme> Theme { get; set; }
        public DbSet<Sujet> Sujets { get; set; }

        public DbSet<User> User { get; set; }

        public DbSet<Message> Messages { get; set; }

        public DbSet<MessageSujetUser> MessageSujetUser { get; set; }

    }
}
