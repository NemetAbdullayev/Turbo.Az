using DataAccessLayer.Abstract;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Concrete.EntityFramework
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        private readonly ApplicationDbContext _dataContext;
        public GenericRepository(ApplicationDbContext dataContext)
        {
            _dataContext = dataContext;
        }
        public async Task<T> Add(T t)
        {
          await  _dataContext.AddAsync<T>(t);
          await  _dataContext.SaveChangesAsync();
            return t;
        }

        public async Task Delete(int id)
        {
            var t= await _dataContext.Set<T>().FindAsync(id);
            _dataContext.Update<T>(t);
            await  _dataContext.SaveChangesAsync();
           
        }

        public async Task<T> Get(int id)
        {
         return  await  _dataContext.Set<T>().FindAsync(id);
        }

        public async Task<List<T>> Get()
        {
            return await _dataContext.Set<T>().ToListAsync();
        }

        public async Task<T> Update(T t)
        {
             _dataContext.Update<T>(t);
            await _dataContext.SaveChangesAsync();
            return t;
        }
    }
}
