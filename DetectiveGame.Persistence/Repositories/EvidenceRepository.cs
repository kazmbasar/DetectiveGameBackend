using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using DetectiveGame.Persistence.Contexts;
using DetectiveGame.Domain.Entities;

using DetectiveGame.Application.Repositories;

namespace DetectiveGame.Persistence.Repositories
{
    public class EvidenceRepository : Repository<Evidence>, IEvidenceRepository
    {
        public EvidenceRepository(DetectiveGameDbContext context) : base(context)
        {
        }

        public async Task<IEnumerable<Evidence>> GetEvidencesByGameIdAsync(Guid gameId)
        {
            return await _context.Evidences
                .Where(e => e.GameId == gameId)
                .OrderByDescending(e => e.CreatedDate)
                .ToListAsync();
        }
    }
} 