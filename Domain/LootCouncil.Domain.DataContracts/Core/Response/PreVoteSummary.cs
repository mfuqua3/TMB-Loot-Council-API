using System;

namespace LootCouncil.Domain.DataContracts.Core.Response;

public class PreVoteSummary
{
    public DateTime Expiration { get; set; }
    public int Id { get; set; }
    public int GuildId { get; set; }
    public int ItemCount { get; set; }
}