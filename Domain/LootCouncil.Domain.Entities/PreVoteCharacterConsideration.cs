using LootCouncil.Domain.Data;

namespace LootCouncil.Domain.Entities;

public class PreVoteCharacterConsideration : IUnique<int>, IOrdered
{
    public int Id { get; set; }
    public int PreVoteCharacterId { get; set; }
    public PreVoteCharacter PreVoteCharacter { get; set; }
    public string Type { get; set; }
    public int Order { get; set; }
}