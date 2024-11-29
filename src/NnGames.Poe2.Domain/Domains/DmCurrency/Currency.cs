using System;
using Volo.Abp.Domain.Entities.Auditing;

namespace NnGames.Poe2.Domains.DmCurrency
{
    public class Currency : AuditedEntity<Guid>
    {
        public string Name { get; set; } = string.Empty;
        public short StackSize { get; set; }
        public string Description { get; set; } = string.Empty;
        public string? Note { get; set; }

        public Currency(string name, short stackSize, string description, string? note = null)
        {
            Name = name;
            StackSize = stackSize;
            Description = description;
            Note = note;
        }
    }
}
