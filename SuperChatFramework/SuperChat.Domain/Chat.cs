using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;

namespace SuperChat.Domain
{
    public class Chat
    {
        public int Id { get; set; }
        public IList<User> Users { get; set; }
        public IList<Message> Messages { get; set; }
        [NotMapped]
        public Dictionary<User, byte[]> Keys { get; set; }

        public string KeysJson {
            get => JsonConvert.SerializeObject(Keys);
            set => JsonConvert.DeserializeObject<Dictionary<User, byte[]>>(value);
        }
    }
}
