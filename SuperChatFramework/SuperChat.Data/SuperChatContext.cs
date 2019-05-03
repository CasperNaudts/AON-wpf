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
            //optionsBuilder.UseSqlServer("Server = (localdb)\\mssqllocaldb; Database = SuperChatAppData; Trusted_Connection = True; ");
            optionsBuilder.UseMySQL("server=cpanel.edu-tech.be; database=casper_securety; userid=casper_edutec1q; pwd=CT{(1vjmC;8&I~#9I7;");
            base.OnConfiguring(optionsBuilder);
        }
    }
}
