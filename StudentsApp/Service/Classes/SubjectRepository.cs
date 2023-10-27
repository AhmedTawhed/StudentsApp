using Microsoft.EntityFrameworkCore;
using StudentsApp.Models;
using StudentsApp.Service.Interfaces;

namespace StudentsApp.Service.Classes
{
    public class SubjectRepository : Repository<Subject>, ISubjectRepository
    {
        public SubjectRepository(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }
        public async Task<IEnumerable<Subject>> GetSubjectsBySubjectIds(IEnumerable<Guid> subjectIds)
        {
            return await this.Context.Set<Subject>().Where(s => subjectIds.Contains(s.Id)).ToListAsync();
        }
        public new async Task<IEnumerable<Subject>> GetAll()
        {
            return await Context.Set<Subject>().Where(s => s.IsDeleted == false).ToListAsync();
        }
    }
}