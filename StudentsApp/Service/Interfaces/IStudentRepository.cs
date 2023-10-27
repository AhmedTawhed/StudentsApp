using StudentsApp.Models;

namespace StudentsApp.Service.Interfaces
{
    public interface IStudentRepository : IRepository<Student>
    {
        new Task<IEnumerable<Student>> GetAll();
        Student GetByCode(int Code);

    }
}
