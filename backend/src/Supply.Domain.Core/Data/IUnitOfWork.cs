using System.Threading.Tasks;

namespace Supply.Domain.Core.Messaging.Data
{
    public interface IUnitOfWork
    {
        Task<bool> Commit();
    }
}
