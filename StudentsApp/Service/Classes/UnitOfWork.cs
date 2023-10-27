using StudentsApp.Data;
using StudentsApp.Service.Interfaces;

namespace StudentsApp.Service.Classes
{
    public class UnitOfWork : IUnitOfWork
    {
        public readonly StudentsDBContext internalDbContext;
        public UnitOfWork(StudentsDBContext dbContext)
        {
            internalDbContext = dbContext;
        }

        StudentsDBContext IUnitOfWork.DbContext => internalDbContext;

        public async Task<int> SaveChangeAsync()
        {
            return await internalDbContext.SaveChangesAsync();
        }
    }
}
