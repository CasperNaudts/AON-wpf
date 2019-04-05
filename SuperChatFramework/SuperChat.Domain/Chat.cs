using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
