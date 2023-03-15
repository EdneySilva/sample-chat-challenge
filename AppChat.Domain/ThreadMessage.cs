using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppChat.Domain
{
    public class ThreadMessage
    {
        public string MessageId { get; set; }

        public string Room { get; set; }

        public string From { get; set; }

        public string To { get; set; }

        public string Content { get; set; }

        public string ContentType { get; set; }

        public DateTime CreatedAt { get; set; }        
        public DateTime? MessageDelivered { get; set; }
        public DateTime? MessageRead { get; set; }

        public virtual Room ChatRoom { get; set; }

        public ThreadMessage ShallowClone()
        {
            var clone = this.MemberwiseClone() as ThreadMessage;
            clone.ChatRoom = null;
            return clone;
        }
    }
}
