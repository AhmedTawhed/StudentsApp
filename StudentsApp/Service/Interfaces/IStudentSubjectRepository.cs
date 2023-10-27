using StudentsApp.Models;

namespace StudentsApp.Service.Interfaces
{
    public interface IStudentSubjectRepository : IRepository<StudentSubject>
    {
        Task AssignSubjectsToStudents(IEnumerable<Guid> studentIds, IEnumerable<Guid> subjectIds);
        Task<IEnumerable<StudentSubject>> GetAllWithDetails();
        Task<IEnumerable<Subject>> GetSubjectsByStudentId(Guid studentId);
        Task<StudentSubject> GetById(Guid studentId, Guid subjectId);
    }
}
