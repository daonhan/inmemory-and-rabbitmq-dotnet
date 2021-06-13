namespace Supply.Domain.Core.Domain
{
    public class DomainMessages
    {
        public static DomainMessage CommitFailed => new DomainMessage("There was an error saving data.");
        public static DomainMessage RequiredField => new DomainMessage("Please, ensure you enter {0}.");
        public static DomainMessage AlreadyInUse => new DomainMessage("The informed {0} is already in use.");
        public static DomainMessage InvalidFormat => new DomainMessage("The informed {0} is invalid.");
        public static DomainMessage NotFound => new DomainMessage("The informed {0} was not found.");
    }
}
