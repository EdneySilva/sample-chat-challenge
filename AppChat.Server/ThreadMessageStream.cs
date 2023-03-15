using AppChat.Domain;
using System.Collections.Concurrent;

namespace AppChat.Server
{
    public class ThreadMessageStream
    {
        BlockingCollection<ThreadMessage> threadMessages = new BlockingCollection<ThreadMessage>();
        public void Append(ThreadMessage message)
        {
            threadMessages.Add(message);
        }

        public ThreadMessage ReadNext(CancellationToken cancellationToken)
        {
            var item = threadMessages.Take(cancellationToken);
            return item;
        }
    }
}
