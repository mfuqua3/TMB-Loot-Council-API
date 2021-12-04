using System.Linq;
using AutoMapper;
using LootCouncil.Domain.DataContracts.Core.Response;
using LootCouncil.Domain.Entities;

namespace LootCouncil.Service.Mapping
{
    public class ImportProfile : Profile
    {
        public ImportProfile()
        {
            CreateMap<Import, ImportResponse>()
                .ForMember(x => x.InitiatedBy, o => o.MapFrom(e => e.User.UserName));
        }
    }
}