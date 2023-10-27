namespace StudentsApp.Service.Interfaces
{
    public interface IRepository<T> where T : class
    {
        Task<T> Add(T entity);
        T Update(T entity);
        void Delete(T entity);
        Task<IEnumerable<T>> GetAll();
        Task<T> GetById(Guid id);
        Task SaveChanges();
    }
}