using System;
using System.Collections.Generic;
using LootCouncil.Domain.DataContracts.Core.Model;

namespace LootCouncil.Domain.DataContracts.Core.Response
{
    public class PreVoteDetail
    {
        public int Id { get; set; }
        public int GuildId { get; set; }
        public List<PreVoteItemModel> Items { get; set; }
        public DateTime Expiration { get; set; }
    }
}