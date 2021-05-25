namespace Supply.Receiver.Models
{
    public class QueueInfo
    {
        public string Name { get; private set; }

        public QueueInfo(string name)
        {
            Name = name;
        }
    }
}
