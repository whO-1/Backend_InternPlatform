
using internPlatform.Domain.Models;
using internPlatform.Infrastructure.Data;
using internPlatform.Infrastructure.Repository.IRepository;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Diagnostics;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace internPlatform.Infrastructure.Repository
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly ProjectDBContext _db;
        internal DbSet<T> dbSet;
        public Repository(ProjectDBContext db)
        {
            _db = db;
            this.dbSet = _db.Set<T>();
        }

        public T Add(T entity)
        {
            try
            {
                return dbSet.Add(entity);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
            return null;

        }

        public async Task<T> GetById(int Id, string includeProperties = null)
        {
            var model = await dbSet.FindAsync(Id);
            if (includeProperties != null)
            {
                foreach (string prop in includeProperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    _db.Entry(model).Reference(prop).Load();
                }
            }

            return model;
        }
        public async Task<T> Get(Expression<Func<T, bool>> filter, string includeProperties = null)
        {
            IQueryable<T> query = dbSet;
            bool result = await query.AnyAsync(filter);
            if (result)
            {
                query = query.Where(filter);
                if (!string.IsNullOrWhiteSpace(includeProperties))
                {
                    foreach (string prop in includeProperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                    {
                        query = query.Include(prop);
                    }
                }

                return await query.FirstOrDefaultAsync();
            }
            return null;
        }
        public IEnumerable<T> GetAll(Expression<Func<T, bool>> filter = null, string includeProperties = null)
        {
            IQueryable<T> query = dbSet;
            if (filter != null)
            {
                query = query.Where(filter);
            }

            if (!string.IsNullOrWhiteSpace(includeProperties))
            {
                foreach (var prop in includeProperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(prop);
                }
            }
            return query;
        }
        public async Task<PaginatedList<T>> GetPaginatedAsync(PaginationOptions options, Expression<Func<T, int>> orderBy)
        {
            IQueryable<T> query = dbSet;
            int count = await query.CountAsync();

            List<T> items = (count < options.PageSize) ?
                        await query.ToListAsync()
                            :
                                await query.OrderBy(orderBy).Skip((options.CurrentPage - 1) * options.PageSize).Take(options.PageSize).ToListAsync();


            return new PaginatedList<T>(items, count, options.CurrentPage, options.PageSize);
        }

        public T Remove(T entity)
        {
            return dbSet.Remove(entity);
        }

        public void RemoveRange(IEnumerable<T> entityRange)
        {
            dbSet.RemoveRange(entityRange);
        }

        public void Update(T entity)
        {
            dbSet.AddOrUpdate(entity);
        }

        public async Task<int> Save()
        {
            try
            {
                return await _db.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
            return 0;
        }
    }
}