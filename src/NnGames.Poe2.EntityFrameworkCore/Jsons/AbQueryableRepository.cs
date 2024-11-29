using Microsoft.EntityFrameworkCore;
using NnGames.Poe2.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Linq.Expressions;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using Volo.Abp.Domain.Entities;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Linq;

namespace NnGames.Poe2.Jsons
{
    public abstract class AbQueryableRepository<TEntity, TKey> : IRepository<TEntity, TKey>
        where TEntity : class, IEntity<TKey>
    {
        public abstract Task<List<TEntity>> LoadAsync();
        public abstract Task SaveAsync(List<TEntity> l);
        public abstract Task<TEntity> ToInsertAsync(TEntity entity);
        public abstract Task<TEntity> ToUpdateAsync(TEntity entity);

        public IAsyncQueryableExecuter AsyncExecuter => throw new NotImplementedException();

        public bool? IsChangeTrackingEnabled => false;

        public async Task DeleteAsync(Expression<Func<TEntity, bool>> predicate, bool autoSave = false, CancellationToken cancellationToken = default)
        {
            var lDelete = await GetListAsync(predicate, autoSave, cancellationToken);
            await DeleteManyAsync(lDelete, autoSave, cancellationToken);
        }

        public async Task DeleteAsync(TKey id, bool autoSave = false, CancellationToken cancellationToken = default)
        {
            var l = await GetListAsync();
            l.RemoveAll(x => x.Id!.Equals(id));

            await SaveAsync(l);
        }

        public async Task DeleteAsync(TEntity entity, bool autoSave = false, CancellationToken cancellationToken = default)
        {
            var l = await GetListAsync();
            l.RemoveAll(x => x.Id!.Equals(entity.Id));

            await SaveAsync(l);
        }

        public async Task DeleteDirectAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken = default)
        {
            var lDelete = await GetListAsync(predicate, cancellationToken: cancellationToken);
            await DeleteManyAsync(lDelete, cancellationToken: cancellationToken);
        }

        public async Task DeleteManyAsync(IEnumerable<TKey> ids, bool autoSave = false, CancellationToken cancellationToken = default)
        {
            var l = await GetListAsync();
            l.RemoveAll(x => ids.Contains(x.Id));

            await SaveAsync(l);
        }

        public async Task DeleteManyAsync(IEnumerable<TEntity> entities, bool autoSave = false, CancellationToken cancellationToken = default)
        {
            var l = await GetListAsync();
            l.RemoveAll(x => entities.Select(x => x.Id).Contains(x.Id));

            await SaveAsync(l);
        }

        public async Task<TEntity?> FindAsync(Expression<Func<TEntity, bool>> predicate, bool includeDetails = true, CancellationToken cancellationToken = default)
        {
            return await (await GetQueryableAsync()).Where(predicate).FirstOrDefaultAsync();
        }

        public async Task<TEntity?> FindAsync(TKey id, bool includeDetails = true, CancellationToken cancellationToken = default)
        {
            return await (await GetQueryableAsync()).Where(x => x.Id!.Equals(id)).FirstOrDefaultAsync();
        }

        public async Task<TEntity> GetAsync(Expression<Func<TEntity, bool>> predicate, bool includeDetails = true, CancellationToken cancellationToken = default)
        {
            return (await FindAsync(predicate, includeDetails, cancellationToken))!;
        }

        public async Task<TEntity> GetAsync(TKey id, bool includeDetails = true, CancellationToken cancellationToken = default)
        {
            return (await FindAsync(id, includeDetails, cancellationToken))!;
        }

        public async Task<long> GetCountAsync(CancellationToken cancellationToken = default)
        {
            return (await GetQueryableAsync()).Count();
        }

        public async Task<List<TEntity>> GetListAsync(Expression<Func<TEntity, bool>> predicate, bool includeDetails = false, CancellationToken cancellationToken = default)
        {
            return (await GetQueryableAsync()).Where(predicate).ToList();
        }

        public async Task<List<TEntity>> GetListAsync(bool includeDetails = false, CancellationToken cancellationToken = default)
        {
            return (await GetQueryableAsync()).ToList();
        }

        public async Task<List<TEntity>> GetPagedListAsync(int skipCount, int maxResultCount, string sorting, bool includeDetails = false, CancellationToken cancellationToken = default)
        {
            return (await GetListAsync()).Skip(skipCount).Take(maxResultCount).AsQueryable().OrderBy(sorting).ToList();
        }

        public async Task<IQueryable<TEntity>> GetQueryableAsync()
        {
            return (await LoadAsync()).AsQueryable();
        }

        public async Task<TEntity> InsertAsync(TEntity entity, bool autoSave = false, CancellationToken cancellationToken = default)
        {
            var l = await GetListAsync();
            l.Add(await ToInsertAsync(entity));

            await SaveAsync(l);
            return entity;
        }

        public async Task InsertManyAsync(IEnumerable<TEntity> entities, bool autoSave = false, CancellationToken cancellationToken = default)
        {
            var lEntity = entities.ToList();
            for (var i = 0; i < lEntity.Count; i++)
                lEntity[i] = await ToInsertAsync(lEntity[i]);

            var l = await GetListAsync();
            l.AddRange(entities);

            await SaveAsync(l);
        }

        public async Task<TEntity> UpdateAsync(TEntity entity, bool autoSave = false, CancellationToken cancellationToken = default)
        {
            var l = await GetListAsync();

            var dbEntity = l.Where(x => x.Id!.Equals(entity.Id)).First();
            dbEntity = JsonUtil.Clone(await ToUpdateAsync(entity));

            await SaveAsync(l);
            return dbEntity!;
        }

        public async Task UpdateManyAsync(IEnumerable<TEntity> entities, bool autoSave = false, CancellationToken cancellationToken = default)
        {
            var l = await GetListAsync();

            foreach (var entity in entities)
            {
                var dbEntity = l.Where(x => x.Id!.Equals(entity.Id)).First();
                dbEntity = JsonUtil.Clone(entity);
            }

            await SaveAsync(l);
        }

        public IQueryable<TEntity> WithDetails()
        {
            throw new NotImplementedException();
        }

        public IQueryable<TEntity> WithDetails(params Expression<Func<TEntity, object>>[] propertySelectors)
        {
            throw new NotImplementedException();
        }

        public Task<IQueryable<TEntity>> WithDetailsAsync()
        {
            throw new NotImplementedException();
        }

        public Task<IQueryable<TEntity>> WithDetailsAsync(params Expression<Func<TEntity, object>>[] propertySelectors)
        {
            throw new NotImplementedException();
        }

    }
}
