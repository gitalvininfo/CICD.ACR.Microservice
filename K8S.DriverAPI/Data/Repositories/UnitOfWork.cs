
using K8S.DriverAPI.Data.Repositories.Interfaces;
using Microsoft.Extensions.Logging;

namespace K8S.DriverAPI.Data.Repositories
{
    public class UnitOfWork : IUnitOfWork, IDisposable
    {
        private readonly AppDbContext _context;

        public IDriverRepository Drivers { get; }

        public UnitOfWork(AppDbContext context, ILoggerFactory loggerFactory)
        {
            _context = context;

            var logger = loggerFactory.CreateLogger("logs");

            Drivers = new DriverRepository(logger, context);
        }

        public async Task<bool> CompleteAsync()
        {
            var result = await _context.SaveChangesAsync();

            return result > 0;
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
