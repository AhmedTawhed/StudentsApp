using StudentsApp.Data;
using StudentsApp.Models;
using StudentsApp.Service.Interfaces;
using System.ComponentModel.DataAnnotations;

namespace StudentsApp.Validations
{
    public class UniqueCodeAttribute : ValidationAttribute
    {
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            StudentsDBContext dbContext = new StudentsDBContext();
            int StudentCode = (int)value;
            Student StudentRequest = validationContext.ObjectInstance as Student;
            Student Studentdb = dbContext.Students.FirstOrDefault(c => c.Code == StudentCode);
            if (Studentdb == null || Studentdb.Id == StudentRequest.Id)
            {
                return ValidationResult.Success;
            }
            return new ValidationResult("Code Already Exists");

        }
    }
    public class UniqueSubjectCodeAttribute : ValidationAttribute
    {
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            StudentsDBContext dbContext = new StudentsDBContext();
            int subjectCode = (int)value;
            Subject SubjectRequest = validationContext.ObjectInstance as Subject;
            Subject SubjectDb = dbContext.Subjects.FirstOrDefault(c => c.Code == subjectCode);
            if (SubjectDb == null || SubjectDb.Id == SubjectRequest.Id)
            {
                return ValidationResult.Success;
            }
            return new ValidationResult("Code Already Exists");

        }
    }
}
