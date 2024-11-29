using System;
using Volo.Abp.Domain.Repositories;

namespace NnGames.Poe2.Domains.DmCurrency
{
    public interface ICurrencyRepository : IRepository<Currency, Guid>
    {

    }
}
