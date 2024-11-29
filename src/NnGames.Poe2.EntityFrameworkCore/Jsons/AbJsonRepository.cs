using Microsoft.Extensions.Configuration;
using NnGames.Poe2.EntityFrameworkCore;
using NnGames.Poe2.Utils;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text.Json;
using System.Threading.Tasks;
using Volo.Abp.Domain.Entities;

namespace NnGames.Poe2.Jsons
{
    public abstract class AbJsonRepository<TEntity, TKey> : AbQueryableRepository<TEntity, TKey>
        where TEntity : class, IEntity<TKey>
    {
        protected readonly IConfiguration _configuration;
        protected readonly string _entityName;

        protected string _embeddedResourcePath;
        protected string _filePath;
        protected bool _useFile;

        public AbJsonRepository(IConfiguration configuration, string entityName)
        {
            _configuration = configuration;
            _entityName = entityName;

            var version = _configuration["JsonRepository:Version"];
            _embeddedResourcePath = _configuration["JsonRepository:EmbeddedResourcePath"]!.Replace("{version}", version).Replace("{entity}", _entityName);
            _filePath = _configuration["JsonRepository:FilePath"]!.Replace("{version}", version).Replace("{entity}", _entityName);
            _useFile = bool.Parse(_configuration["JsonRepository:UseFile"]!);
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

        protected Task<List<TEntity>> LoadFileAsync()
        {
            return Task.FromResult(JsonUtil.ReadFromJsonFile<List<TEntity>>(_filePath));
        }

        protected Task SaveFileAsync(List<TEntity> l)
        {
            JsonUtil.WriteToJsonFile(_filePath, l);
            return Task.CompletedTask;
        }
    }
}
