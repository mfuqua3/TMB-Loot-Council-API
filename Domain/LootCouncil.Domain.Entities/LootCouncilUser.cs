using System;
using System.Collections.Generic;
using LootCouncil.Domain.Data;
using Microsoft.AspNetCore.Identity;

namespace LootCouncil.Domain.Entities
{
    public class LootCouncilUser : IdentityUser, IUnique<string>, ITracked
    { 
        public DiscordIdentity DiscordIdentity { get; set; }
        public List<GuildUser> GuildUsers { get; set; }
        public DateTime Created { get; set; }
        public DateTime? Updated { get; set; }
    }
}