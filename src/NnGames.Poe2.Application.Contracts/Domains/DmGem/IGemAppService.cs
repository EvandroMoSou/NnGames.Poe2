using System;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace NnGames.Poe2.Domains.DmGem
{
    public interface IGemAppService : ICrudAppService<GemDto, Guid, PagedAndSortedResultRequestDto> 
    {

    }
}
