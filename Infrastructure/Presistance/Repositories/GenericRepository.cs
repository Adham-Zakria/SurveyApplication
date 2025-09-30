using Domain.Contracts;
using Domain.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Presistance.Repositories
{
    public class GenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : class
    {

        //#region Get

        //public TEntity? GetById(int id)
        //    => _appContext.Set<TEntity>().Find(id);

        //public IEnumerable<TEntity> GetAll()
        //    => _appContext.Set<TEntity>().ToList();

        //public IEnumerable<TResult> GetAll<TResult>(Expression<Func<TEntity, TResult>> selector)
        //{
        //    return _appContext.Set<TEntity>()
        //        .Select(selector);
        //}

        //public IEnumerable<TEntity> GetAll(Expression<Func<TEntity, bool>> filter)
        //{
        //    return _appContext.Set<TEntity>()
        //              .Where(filter)
        //              .ToList();
        //}

        //#endregion

        //public int Add(TEntity entity)
        //{
        //    _appContext.Set<TEntity>().Add(entity);
        //    return _appContext.SaveChanges();
        //}

        //public int Update(TEntity entity)
        //{
        //    _appContext.Set<TEntity>().Update(entity);
        //    return _appContext.SaveChanges();
        //}

        //public int Remove(TEntity entity)
        //{
        //    _appContext.Set<TEntity>().Remove(entity);
        //    return _appContext.SaveChanges();
        //}

        private readonly SurveyAppContext _context;
        private readonly DbSet<TEntity> _dbSet;

        public GenericRepository(SurveyAppContext context)
        {
            _context = context;
            _dbSet = _context.Set<TEntity>();
        }

        public async Task<IEnumerable<TEntity>> GetAllAsync()
        {
            return await _dbSet.ToListAsync();
        }

        public async Task<TEntity> GetByIdAsync(object id)
        {
            return await _dbSet.FindAsync(id);
        }

        public async Task AddAsync(TEntity entity)
        {
            await _dbSet.AddAsync(entity);
        }

        public void Update(TEntity entity)
        {
            _dbSet.Update(entity);
        }

        public void Delete(TEntity entity)
        {
            _dbSet.Remove(entity);
        }

        public async Task SaveAsync()
        {
            await _context.SaveChangesAsync();
        }

    }
}
