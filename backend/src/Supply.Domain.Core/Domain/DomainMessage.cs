namespace Supply.Domain.Core.Domain
{
    public class DomainMessage
    {
        public string Message { get; }

        public DomainMessage(string message)
        {
            Message = message;
        }

        public DomainMessage Format(params object[] args)
        {
            return new DomainMessage(string.Format(Message, args));
        }
    }
}
