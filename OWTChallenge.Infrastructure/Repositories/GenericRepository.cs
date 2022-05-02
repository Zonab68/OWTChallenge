using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using OWTChallenge.Infrastructure.Data;
using OWTChallenge.SharedKernel.Interfaces;

namespace OWTChallenge.Infrastructure.Repositories;

public class GenericRepository<T> : IGenericRepository<T> where T : class
{

    #region Properties
    protected readonly OWTChallengeContext context;
    #endregion

    public GenericRepository(OWTChallengeContext context)
    {
        this.context = context;
    }
    public void Add(T entity)
    {
        context.Set<T>().Add(entity);
    }

    public async Task<T> AddAsync(T t)
    {
        context.Set<T>().Add(t);
        await context.SaveChangesAsync();
        return t;
    }

    public void AddRange(IEnumerable<T> entities)
    {
        context.Set<T>().AddRange(entities);
    }
    public IEnumerable<T> Find(Expression<Func<T, bool>> expression)
    {
        return context.Set<T>().Where(expression);
    }
    public IEnumerable<T> GetAll()
    {
        return context.Set<T>().ToList();
    }
    public T GetById(int id)
    {
        return context.Set<T>().Find(id);
    }
    public void Delete(T entity)
    {
        context.Set<T>().Remove(entity);
    }
    public async Task<int> DeletedAsync(T t)
    {
        context.Set<T>().Remove(t);
        return await context.SaveChangesAsync();
    }
    public void DeleteRange(IEnumerable<T> entities)
    {
        context.Set<T>().RemoveRange(entities);
    }
}
