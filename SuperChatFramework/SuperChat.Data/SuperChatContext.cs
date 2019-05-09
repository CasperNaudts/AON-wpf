using System.Configuration;
using Microsoft.EntityFrameworkCore;
using SuperChat.Domain;

namespace SuperChat.Data
{
    public class SuperChatContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Message> Messages { get; set; }
        public DbSet<Chat> Chats { get; set; }
        public DbSet<Key> Keys { get; set; }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseMySql(ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString);
            base.OnConfiguring(optionsBuilder);
        }
    }
}
