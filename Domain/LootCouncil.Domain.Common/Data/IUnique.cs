using System;
using System.Text.Json.Serialization;

namespace LootCouncil.Domain.Data
{
    public interface IUnique<T> where T:IEquatable<T>, IComparable<T>
    {
        T Id { get; set; }
    }
}