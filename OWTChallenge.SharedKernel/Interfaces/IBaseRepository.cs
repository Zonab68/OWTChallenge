namespace OWTChallenge.SharedKernel.Interfaces
{
    /// <summary>
    /// Repository interface that will serve us all the CRUD methods
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    /// 

    public interface IBaseRepository<TEntity>
    {
        ///
        IEnumerable<TEntity> GetAll();
        Task<IEnumerable<TEntity>> GetAllAsync();
        TEntity Get(int id);
        Task<TEntity> GetAsync(int id);
        TEntity Add(TEntity entity);
        TEntity Update(TEntity entityToUpdate, int key);
        void Delete(TEntity entity);

    }
}