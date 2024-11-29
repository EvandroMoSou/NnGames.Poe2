using NnGames.PoE2.OpenDb.Enums;
using System;
using Volo.Abp.Domain.Entities.Auditing;

namespace NnGames.Poe2.Domains.DmGem
{
    public class Gem : AuditedEntity<Guid>
    {
        public string Name { get; set; } = string.Empty;
        public string Type { get; set; } = string.Empty;
    }
}
