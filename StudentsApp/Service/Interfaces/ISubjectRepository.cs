using StudentsApp.Models;

namespace StudentsApp.Service.Interfaces
{
    public interface ISubjectRepository : IRepository<Subject>
    {
        Task<IEnumerable<Subject>> GetSubjectsBySubjectIds(IEnumerable<Guid> subjectIds);
        new Task<IEnumerable<Subject>> GetAll();
    }
}
