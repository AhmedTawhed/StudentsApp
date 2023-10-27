using Microsoft.EntityFrameworkCore;
using StudentsApp.Models;
using StudentsApp.Service.Interfaces;

namespace StudentsApp.Service.Classes
{
    public class StudentRepository : Repository<Student>, IStudentRepository
    {
        public StudentRepository(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }
        public new async Task<IEnumerable<Student>> GetAll()
        {
            return await Context.Set<Student>().Where(s => s.IsDeleted == false).ToListAsync();
        }

        public Student GetByCode(int Code)
        {
            return  Context.Set<Student>().FirstOrDefault(s => s.Code == Code);
        }
    }
}
