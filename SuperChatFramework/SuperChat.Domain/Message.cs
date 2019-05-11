using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace SuperChat.Domain
{
    public class Message
    {
        public int Id { get; set; }
        public string Content { get; set; }
        public int SenderId { get; set; }
        public int RecieverId { get; set; }
        // 1 Reciever, indien groupchats worden toegevoegd moet dit naar een list worden omgezet
        public DateTime TimeSend { get; set; }
        public byte[] Iv { get; set; }

        public override string ToString()
        {
            return Content;
        }
    }
}