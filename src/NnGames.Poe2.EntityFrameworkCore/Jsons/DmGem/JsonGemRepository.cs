using Microsoft.Extensions.Configuration;
using NnGames.Poe2.Domains.DmGem;
using NnGames.Poe2.Utils;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Volo.Abp.Auditing;
using Volo.Abp.Guids;

namespace NnGames.Poe2.Jsons.DmGem
{
    public class JsonGemRepository : AbGuidJsonRepository<Gem>, IGemRepository
    {
        public JsonGemRepository(
            IAuditPropertySetter auditPropertySetter,
            IConfiguration configuration,
            IGuidGenerator guidGenerator) : base(auditPropertySetter, configuration, guidGenerator, "Gem")
        {

        }

        public async Task InsertOrUpdateByNameAsync(List<Gem> lEntity)
        {
            var l = await GetListAsync();

            foreach (var iEntity in lEntity)
            {
                var entity = l.Where(x => x.Name == iEntity.Name).FirstOrDefault();
                if (entity == null)
                    l.Add(await ToInsertAsync(iEntity));
                else
                    entity = JsonUtil.Clone(await ToUpdateAsync(entity));
            }

            await SaveAsync(l);
        }

    }
}
