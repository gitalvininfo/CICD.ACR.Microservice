
namespace K8S.DriverAPI.Data.Repositories.Interfaces
{
    public interface IUnitOfWork
    {
        IDriverRepository Drivers { get; }

        Task<bool> CompleteAsync();
    }
}
