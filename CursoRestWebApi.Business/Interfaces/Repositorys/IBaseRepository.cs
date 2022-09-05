using CursoRestWebApi.Business.Models;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace CursoRestWebApi.Business.Interfaces.Repositorys
{
    public interface IBaseRepository<TEntity> : IDisposable where TEntity : Entity
    {
        Task<IEnumerable<TEntity>> GetAll();
        Task<IEnumerable<TEntity>> GetAllFilter(Expression<Func<TEntity, bool>> where);
        Task<TEntity> GetById(Guid id);
        Task<TEntity> GetByFilter(Expression<Func<TEntity, bool>> where);
        Task<TEntity> Add(TEntity entity);
        Task<TEntity> Update(TEntity entity);
        Task Remove(Guid id);
        Task<bool> SaveChangesAsync();
    }
}
