using System.ComponentModel.DataAnnotations;

namespace StudentsApp.ViewModels
{
    public class StudentWithSubjectsViewModel
    {
        [Required]
        public IEnumerable<Guid> StudentIds { get; set; }
        [Required]
        public IEnumerable<Guid> SubjectIds { get; set; }
        public IEnumerable<int> Grade { get; set; }

        public IEnumerable<(Guid subject, int grade)> SubjectsAndGrades { get; set; }
    }
}