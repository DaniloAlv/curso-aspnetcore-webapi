using CursoRestWebApi.Business.Interfaces.Repositorys;
using CursoRestWebApi.Business.Models;
using CursoRestWebApi.Data.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace CursoRestWebApi.Data.Repositorys
{
    public abstract class BaseRepository<TEntity> : IBaseRepository<TEntity> where TEntity : Entity
    {
        protected readonly CursoRestWebApiDbContext _apiDbContext;

        protected BaseRepository(CursoRestWebApiDbContext apiDbContext)
        {
            _apiDbContext = apiDbContext;
        }

        public async Task<IEnumerable<TEntity>> GetAll()
        {
            return await _apiDbContext.Set<TEntity>().AsNoTracking().ToListAsync();
        }

        public async Task<IEnumerable<TEntity>> GetAllFilter(Expression<Func<TEntity, bool>> where)
        {
            return await _apiDbContext.Set<TEntity>()
                .AsNoTracking()
                .Where(where)
                .ToListAsync();
        }

        public async Task<TEntity> GetByFilter(Expression<Func<TEntity, bool>> where)
        {
            return await _apiDbContext.Set<TEntity>()
                .AsNoTracking()
                .FirstOrDefaultAsync(where);
        }

        public async Task<TEntity> GetById(Guid id)
        {
            return await _apiDbContext.Set<TEntity>().FindAsync(id);
        }

        public async Task<TEntity> Add(TEntity entity)
        {
            await _apiDbContext.Set<TEntity>().AddAsync(entity);
            await SaveChangesAsync();

            return entity;
        }

        public async Task<TEntity> Update(TEntity entity)
        {
            _apiDbContext.Set<TEntity>().Update(entity);
            await SaveChangesAsync();

            return entity;
        }

        public async Task Remove(Guid id)
        {
            var entity = await GetById(id);
            _apiDbContext.Remove(entity);

            await SaveChangesAsync();
        }

        public async Task<bool> SaveChangesAsync()
        {
            return await _apiDbContext.SaveChangesAsync() > 0;
        }

        public async void Dispose()
        {
            await _apiDbContext.DisposeAsync();
        }
    }
}
