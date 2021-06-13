using System;

namespace Supply.Receiver.Models
{
    public class QueueInfo
    {
        public Type Type { get; private set; }
        public string Name { get; private set; }

        public QueueInfo(Type type)
        {
            Type = type;
            Name = type.Name;
        }
    }
}
