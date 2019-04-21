namespace SuperChat.Domain
{
    public class Key
    {
        public int Id { get; set; }
        public int ChatId { get; set; }
        public Chat Chat { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
        public byte[] KeyBytes { get; set; }

        public Key()
        {

        }
    }
}