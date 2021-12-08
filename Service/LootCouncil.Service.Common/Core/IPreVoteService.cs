﻿using System.Threading.Tasks;
using LootCouncil.Domain.DataContracts.Core.Request;
using LootCouncil.Domain.DataContracts.Core.Response;

namespace LootCouncil.Service.Core
{
    public interface IPreVoteService
    {
        Task<PreVoteResponse> CreatePreVote(CreatePreVoteRequest request);
    }
}