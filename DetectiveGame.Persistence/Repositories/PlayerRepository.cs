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
    public class PlayerRepository : Repository<Player>, IPlayerRepository
    {
        public PlayerRepository(DetectiveGameDbContext context) : base(context)
        {
        }

        public async Task<Player> GetByConnectionIdAsync(string connectionId)
        {
            return await _context.Players
                .FirstOrDefaultAsync(p => p.ConnectionId == connectionId);
        }

        public async Task<IEnumerable<Player>> GetPlayersByGameIdAsync(Guid gameId)
        {
            return await _context.Players
                .Where(p => p.GameId == gameId)
                .ToListAsync();
        }
    }
} 