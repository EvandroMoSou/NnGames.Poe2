using Microsoft.Extensions.Configuration;
using System;
using System.Threading.Tasks;
using Volo.Abp.Auditing;
using Volo.Abp.Domain.Entities;
using Volo.Abp.Guids;

namespace NnGames.Poe2.Jsons
{
    public abstract class AbGuidJsonRepository<TEntity> : AbJsonRepository<TEntity, Guid>
        where TEntity : class, IEntity<Guid>
    {
        protected readonly IAuditPropertySetter _auditPropertySetter;
        protected readonly IGuidGenerator _guidGenerator;

        public AbGuidJsonRepository(
            IAuditPropertySetter auditPropertySetter,
            IConfiguration configuration,
            IGuidGenerator guidGenerator,
            string entityName) : base(configuration, entityName)
        {
            _auditPropertySetter = auditPropertySetter;
            _guidGenerator = guidGenerator;
        }

        public override Task<TEntity> ToInsertAsync(TEntity entity)
        {
            EntityHelper.TrySetId(entity, () => _guidGenerator.Create(), true);
            _auditPropertySetter.SetCreationProperties(entity);

            return Task.FromResult(entity);
        }

        public override Task<TEntity> ToUpdateAsync(TEntity entity)
        {
            _auditPropertySetter.SetModificationProperties(entity);

            return Task.FromResult(entity);
        }
    }
}
