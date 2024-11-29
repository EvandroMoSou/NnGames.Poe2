using NnGames.Poe2.EntityFrameworkCore;
using NnGames.Poe2.Utils;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text.Json;
using System.Threading.Tasks;
using Volo.Abp.Domain.Entities;

namespace NnGames.Poe2.Jsons
{
    public abstract class AbJsonRepository<TEntity, TKey> : AbQueryableRepository<TEntity, TKey>
        where TEntity : class, IEntity<TKey>
    {
        protected readonly string _embeddedResourcePath;
        protected readonly string _filePath;
        protected readonly bool _useFile;

        public AbJsonRepository(
            string embeddedResourcePath,
            string filePath,
            bool useFile)
        {
            _embeddedResourcePath = embeddedResourcePath;
            _filePath = filePath;
            _useFile = useFile;
        }

        public override Task<List<TEntity>> LoadAsync()
        {
            if (_useFile)
                return LoadFileAsync();
            else
                return LoadEmbeddedResourceAsync();
        }

        public override Task SaveAsync(List<TEntity> l)
        {
            if (_useFile)
                return SaveFileAsync(l);
            else
                return SaveEmbeddedResourceAsync(l);
        }

        protected Task<List<TEntity>> LoadEmbeddedResourceAsync()
        {
            var json = EmbeddedResourceUtil.ReadResource(_embeddedResourcePath, Assembly.GetAssembly(typeof(Poe2EntityFrameworkCoreModule))!);
            return Task.FromResult(JsonSerializer.Deserialize<List<TEntity>>(json))!;
        }

        protected Task SaveEmbeddedResourceAsync(List<TEntity> l)
        {
            throw new NotImplementedException();
        }

        protected async Task<List<TEntity>> LoadFileAsync()
        {
            var file = await File.ReadAllTextAsync(_filePath);
            return JsonSerializer.Deserialize<List<TEntity>>(file)!;
        }

        protected async Task SaveFileAsync(List<TEntity> l)
        {
            await File.WriteAllTextAsync(_filePath, JsonSerializer.Serialize(l));
        }
    }
}
