using Microsoft.EntityFrameworkCore;
using StudentsApp.Data;
using StudentsApp.Models;
using StudentsApp.Service.Classes;
using StudentsApp.Service.Interfaces;

namespace StudentsApp
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();
            builder.Services.AddDbContext<StudentsDBContext>(options =>
            options.UseSqlServer(builder.Configuration.GetConnectionString("StudentContext")));

            builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

            #region Repository injection

            builder.Services.AddScoped<IRepository<Student>, Repository<Student>>();
            builder.Services.AddScoped<IRepository<Subject>, Repository<Subject>>();
            builder.Services.AddScoped<IRepository<StudentSubject>, Repository<StudentSubject>>();
            builder.Services.AddScoped<IStudentSubjectRepository, StudentSubjectRepository>();
            builder.Services.AddScoped<ISubjectRepository, SubjectRepository>();
            builder.Services.AddScoped<IStudentRepository, StudentRepository>();

            #endregion

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Run();
        }
    }
}