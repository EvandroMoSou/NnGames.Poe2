using System;
using Volo.Abp.Application.Dtos;

namespace NnGames.Poe2.Domains.DmGem
{
    public class GemDto : AuditedEntityDto<Guid>
    {
        public string Nome { get; set; } = string.Empty;
        public string Type { get; set; } = string.Empty;
    }
}
