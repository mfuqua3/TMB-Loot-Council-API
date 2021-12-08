using System;

namespace LootCouncil.Domain.DataContracts.Core.Model
{
    public class ObjectionModel
    {
        public string User { get; set; }
        public string Reason { get; set; }
        public DateTime TimeStamp { get; set; }
    }
}