using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;

namespace NnGames.Poe2.Domains.DmGem
{
    public interface IGemRepository : IRepository<Gem, Guid>
    {
        Task InsertOrUpdateByNameAsync(List<Gem> lEntity);
    }

}
