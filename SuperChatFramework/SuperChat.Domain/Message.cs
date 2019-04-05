using System;

namespace SuperChat.Domain
{
    public class Message
    {
        public int Id { get; set; }
        public string Content { get; set; }
        public User Sender { get; set; }
        public User Reciever { get; set; }
        // 1 Reciever, indien groupchats worden toegevoegd moet dit naar een list worden omgezet
        public DateTime TimeSend { get; set; }
        public byte[] IV { get; set; }
    }
}