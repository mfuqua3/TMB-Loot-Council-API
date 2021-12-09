using System.Collections.Generic;
using LootCouncil.Domain.Data;

namespace LootCouncil.Domain.Entities;

public class PreVoteCharacter : IUnique<int>
{
    public int Id { get; set; }
    public int PreVoteItemId { get; set; }
    public PreVoteItem PreVoteItem { get; set; }
    public int CharacterId { get; set; }
    public Character Character { get; set; }
    public List<PreVoteCharacterConsideration> CharacterConsiderations { get; set; }
}