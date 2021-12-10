using System;

namespace LootCouncil.Domain.DataContracts.Core.Model
{
    public class ExpirationConfigurationModel
    {
        //TTE means time till expiration
        public DateTime ExpirationTime { get; set; }
        public int LockCommentsTteMinutes { get; set; }
        public int LockObjectionsTteMinutes { get; set; }
        public int LockVotesTteMinutes { get; set; }
    }
}