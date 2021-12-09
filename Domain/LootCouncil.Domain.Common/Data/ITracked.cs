using System;

namespace LootCouncil.Domain.Data
{
    public interface ITracked
    {
        DateTime Created { get; set; }
        DateTime? Updated { get; set; }
    }

}