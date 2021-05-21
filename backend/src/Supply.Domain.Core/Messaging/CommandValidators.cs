using FluentValidation;

namespace Supply.Domain.Core.Messaging
{
    public abstract class CommandValidator<T> : AbstractValidator<T> where T : Command
    {
        protected CommandValidator()
        {
        }
    }
}
