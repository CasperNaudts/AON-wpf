using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;

namespace SuperChat.Domain
{
    public class Chat
    {
        public int Id { get; set; }
        public IList<Message> Messages { get; set; }
        public IList<Key> Keys { get; set; }

        public Chat()
        {
            Messages = new List<Message>();
            Keys = new List<Key>();
        }
    }
}
