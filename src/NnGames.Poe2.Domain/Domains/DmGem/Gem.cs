using System;
using System.Collections.Generic;
using Volo.Abp.Domain.Entities.Auditing;

namespace NnGames.Poe2.Domains.DmGem
{
    public class Gem : AuditedEntity<Guid>
    {
        public string Name { get; set; } = string.Empty;
        public string Type { get; set; } = string.Empty;
        public List<string> TagList { get; set; } = new List<string>();
        public string AttackDamage { get; set; } = string.Empty;
        public string AttackSpeed { get; set; } = string.Empty;
        public string AttackTime { get; set; } = string.Empty;
        public string CastTime { get; set; } = string.Empty;
        public string CooldownTime { get; set; } = string.Empty;
        public string Cost { get; set; } = string.Empty;
        public string CostReservationMultiplier { get; set; } = string.Empty;
        public string CostMultiplier { get; set; } = string.Empty;
        public string CostTime { get; set; } = string.Empty;
        public string CriticalStrikeChance { get; set; } = string.Empty;
        public string Level { get; set; } = string.Empty;
        public string Reservation { get; set; } = string.Empty;
        public string ReservationMultiplier { get; set; } = string.Empty;
        public string Tier { get; set; } = string.Empty;
        public string UseTime { get; set; } = string.Empty;

    }
}
