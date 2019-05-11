using System.ComponentModel.DataAnnotations.Schema;

namespace SuperChat.Domain
{
    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Password { get; set; }
        public string PublicKey { get; set; }
        public string Salt { get; set; }

        public override string ToString()
        {
            return Name;
        }
    }
}