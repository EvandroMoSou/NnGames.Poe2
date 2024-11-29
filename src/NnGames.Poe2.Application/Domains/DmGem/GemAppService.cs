using Microsoft.AspNetCore.Mvc;
using NnGames.Poe2.DataScrapings;
using System;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace NnGames.Poe2.Domains.DmGem
{
    public class GemAppService : CrudAppService<Gem, GemDto, Guid, PagedAndSortedResultRequestDto>, IGemAppService
    {
        protected readonly IGemDataScraping _dataScraping;

        public GemAppService(
            IGemDataScraping dataScraping,
            IGemRepository repository) : base(repository)
        {
            _dataScraping = dataScraping;

            //GetPolicyName = Poe2Permissions.PrmGems.Default;
            //GetListPolicyName = Poe2Permissions.PrmGems.Default;
            //CreatePolicyName = Poe2Permissions.PrmGems.Create;
            //UpdatePolicyName = Poe2Permissions.PrmGems.Edit;
            //DeletePolicyName = Poe2Permissions.PrmGems.Delete;
        }

        [HttpGet]
        public async Task ExecuteDataScraping()
        {
            await _dataScraping.InsertOrUpdateAsync();
        }

    }
}
