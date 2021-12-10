using System;

namespace LootCouncil.Domain.DataContracts.Core.Model
{
    public class CommentModel
    {
        public string User { get; set; }
        public string Comment { get; set; }
        public DateTime TimeStamp { get; set; }
    }
}