using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OWTChallenge.SharedKernel.Interfaces;
using OWTChallenge.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace OWTChallenge.Infrastructure.Repositories
{
    public class BaseRepository<TEntity> : IBaseRepository<TEntity> where TEntity : class, new()
    {
        #region Fields
        private readonly OWTChallengeContext _context = null;
        #endregion

        public BaseRepository(OWTChallengeContext context)
        {
            _context = context;
        }

        public IEnumerable<TEntity> GetAll()
        {
            return _context.Set<TEntity>();
        }

        public async Task<IEnumerable<TEntity>> GetAllAsync()
        {
            var result = await _context.Set<TEntity>().ToListAsync();
            return result;
        }

        public TEntity Get(int id)
        {
            return _context.Set<TEntity>().Find(id);
        }

        public async Task<TEntity> GetAsync(int id)
        {
            return await _context.Set<TEntity>().FindAsync(id);
        }

        public TEntity Find(Expression<Func<TEntity, bool>> match)
        {
            return _context.Set<TEntity>().SingleOrDefault(match);
        }

        public async Task<TEntity> FindAsync(Expression<Func<TEntity, bool>> match)
        {
            return await _context.Set<TEntity>().SingleOrDefaultAsync(match);
        }

        public ICollection<TEntity> FindAll(Expression<Func<TEntity, bool>> match)
        {
            return _context.Set<TEntity>().Where(match).ToList();
        }

        public async Task<ICollection<TEntity>> FindAllAsync(Expression<Func<TEntity, bool>> match)
        {
            return await _context.Set<TEntity>().Where(match).ToListAsync();
        }

        public TEntity Add(TEntity t)
        {
            _context.Set<TEntity>().Add(t);
            _context.SaveChanges();
            return t;
        }

        public async Task<TEntity> AddAsync(TEntity t)
        {
            _context.Set<TEntity>().Add(t);
            await _context.SaveChangesAsync();
            return t;
        }

        public TEntity Update(TEntity updated, int key)
        {
            if (updated == null)
                return null;

            TEntity existing = _context.Set<TEntity>().Find(key);
            if (existing != null)
            {
                _context.Entry(existing).CurrentValues.SetValues(updated);
                _context.SaveChanges();
            }
            return existing;
        }

        public async Task<TEntity> UpdateAsync(TEntity updated, int key)
        {
            if (updated == null)
                return null;

            TEntity existing = await _context.Set<TEntity>().FindAsync(key);
            if (existing != null)
            {
                _context.Entry(existing).CurrentValues.SetValues(updated);
                await _context.SaveChangesAsync();
            }
            return existing;
        }

        public void Delete(TEntity t)
        {
            _context.Set<TEntity>().Remove(t);
            _context.SaveChanges();
        }

        public async Task<int> DeleteAsync(TEntity t)
        {
            _context.Set<TEntity>().Remove(t);
            return await _context.SaveChangesAsync();
        }

        public int Count()
        {
            return _context.Set<TEntity>().Count();
        }

        public async Task<int> CountAsync()
        {
            return await _context.Set<TEntity>().CountAsync();
        }
    }
}
