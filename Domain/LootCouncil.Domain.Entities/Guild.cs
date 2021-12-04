using System;
using System.Collections.Generic;
using LootCouncil.Domain.Data;

namespace LootCouncil.Domain.Entities
{
    public class Guild : IUnique<int>, INamed, ITracked
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime Created { get; set; }
        public DateTime? Updated { get; set; }
        public List<GuildUser> GuildUsers { get; set; }
        public List<Import> Imports { get; set; }
        public int? ConfigurationId { get; set; }
        public GuildConfiguration Configuration { get; set; }
        public GuildServerAssociation ServerAssociation { get; set; }
    }
}