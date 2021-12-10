using System;
using LootCouncil.Domain.Data;

namespace LootCouncil.Domain.Entities
{
    public class ExpirationConfiguration: IUnique<int>
    {
        //TTE means time till expiration
        public DateTime ExpirationTime { get; set; }
        public int LockCommentsTteMinutes { get; set; }
        public int LockObjectionsTteMinutes { get; set; }
        public int LockVotesTteMinutes { get; set; }
        public int Id { get; set; }
    }
}