using NnGames.Poe2.Domains.DmGem;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

namespace NnGames.Poe2.Jsons.DmGem
{
    public class JsonGemRepository : AbJsonRepository<Gem, Guid>, IGemRepository
    {
        public JsonGemRepository() : base("NnGames.Poe2.Jsons.DmGem.gem.json") { }

        public async Task InsertOrUpdateByNameAsync(List<Gem> lEntity)
        {
            var l = await GetListAsync();

            foreach (var iEntity in lEntity)
            {
                var entity = l.Where(x => x.Name == iEntity.Name).FirstOrDefault();
                if (entity == null)
                    l.Add(iEntity);
                else
                    entity = JsonSerializer.Deserialize<Gem>(JsonSerializer.Serialize(entity));
            }

            await SaveAsync(l);
        }
    }
}
