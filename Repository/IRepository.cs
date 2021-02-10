using System;   
using System.Threading.Tasks;

namespace TodoApi.Repository
{
    public interface IRepository<TEntity> where TEntity : class
    {
        TEntity GetById(Object id);

        void Create(TEntity entity);

        void Delete(TEntity entity);

        void Update(TEntity entity);

        void Insert();

        Task<TEntity> GetAsync();
    }
}