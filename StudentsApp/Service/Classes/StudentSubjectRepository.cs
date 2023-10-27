using Microsoft.EntityFrameworkCore;
using StudentsApp.Models;
using StudentsApp.Service.Interfaces;
using System.Transactions;

namespace StudentsApp.Service.Classes
{
    public class StudentSubjectRepository : Repository<StudentSubject>, IStudentSubjectRepository
    {
        private readonly ISubjectRepository SubjectRepository;

        public StudentSubjectRepository(IUnitOfWork unitOfWork, ISubjectRepository subjectRepository) : base(unitOfWork)
        {
            SubjectRepository = subjectRepository;
        }

        public async Task AssignSubjectsToStudents(IEnumerable<Guid> studentIds, IEnumerable<Guid> subjectIds)
        {
            using TransactionScope scope = new(TransactionScopeAsyncFlowOption.Enabled);

            try
            {
                foreach (var studentId in studentIds)
                {
                    foreach (var subjectId in subjectIds)
                    {
                        await this.Context.AddAsync(new StudentSubject()
                        {
                            StudentId = studentId,
                            SubjectId = subjectId
                        });

                       await this.SaveChanges();
                    }
                }
                scope.Complete();
            }
            catch (Exception)
            {
                scope.Dispose();    
            }
        }
        public async Task<IEnumerable<StudentSubject>> GetAllWithDetails()
        {
            return await Context.Set<StudentSubject>().Include(s => s.Student).Include(s => s.Subject)
                .Where(ss => (bool)!ss.Student.IsDeleted).Where(ss => (bool)!ss.Subject.IsDeleted).ToListAsync();
        }

        public async Task<IEnumerable<Subject>> GetSubjectsByStudentId(Guid studentId)
        {
            var studentSubjectIds = await this.Context.Set<StudentSubject>().Include(s => s.Student).Where(s => s.StudentId == studentId).Select(s => s.SubjectId).ToListAsync();
            return await this.SubjectRepository.GetSubjectsBySubjectIds(studentSubjectIds);
        }
        public async Task<StudentSubject> GetById(Guid studentId, Guid subjectId)
        {
            return await Context.Set<StudentSubject>().FindAsync(studentId, subjectId);
        }
    }
}
