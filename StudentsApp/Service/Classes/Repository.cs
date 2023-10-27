using Microsoft.EntityFrameworkCore;
using StudentsApp.Data;
using StudentsApp.Models;
using StudentsApp.Service.Interfaces;

namespace StudentsApp.Service.Classes
{
    public class Repository<T> :IRepository<T> where T : class
    {
        public readonly StudentsDBContext Context;
        private readonly IUnitOfWork UnitOfWork;

        public Repository(IUnitOfWork unitOfWork)
        {
            this.UnitOfWork = unitOfWork;
            this.Context = unitOfWork.DbContext;
        }

        public async Task<T> Add(T entity)
        {
            await Context.AddAsync(entity);
            
            return entity;
        }

        public T Update(T entity)
        {
            Context.Update(entity);

            return entity;
        }

        public void Delete(T entity)
        {
            Context.Remove(entity);
        }

        public async Task<IEnumerable<T>> GetAll()
        {
            return await Context.Set<T>().ToListAsync();
        }

        public async Task<T> GetById(Guid id)
        {
            return await Context.Set<T>().FindAsync(id);
        }

        public async Task SaveChanges()
        {
            await this.Context.SaveChangesAsync();
        }
    }
}