

using K8S.DriverAPI.Models;

namespace K8S.DriverAPI.Data.Repositories.Interfaces
{
    public interface IAchievementRepository : IGenericRepository<Achievement>
    {
        Task<Achievement?> GetDriverAchievementAsync(Guid driverId);
    }
}
