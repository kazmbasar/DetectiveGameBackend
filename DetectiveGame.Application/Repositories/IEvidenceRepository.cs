using DetectiveGame.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;


namespace DetectiveGame.Application.Repositories
{
    public interface IEvidenceRepository : IRepository<Evidence>
    {
        Task<IEnumerable<Evidence>> GetEvidencesByGameIdAsync(Guid gameId);
    }
} 