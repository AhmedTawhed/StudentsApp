using StudentsApp.Data;

namespace StudentsApp.Service.Interfaces
{
    public interface IUnitOfWork
    {
        StudentsDBContext DbContext { get; }
        Task<int> SaveChangeAsync();
    }
}