using Microsoft.EntityFrameworkCore;
using SuperChat.Domain;

namespace SuperChat.Data
{
    public class SuperChatContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Message> Messages { get; set; }
        public DbSet<Chat> Chats { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server = (localdb)\\mssqllocaldb; Database = SuperChatAppData; Trusted_Connection = True; ");
            base.OnConfiguring(optionsBuilder);
        }
    }
}
