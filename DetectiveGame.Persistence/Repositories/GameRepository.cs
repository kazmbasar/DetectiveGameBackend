using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

using DetectiveGame.Persistence.Contexts;
using DetectiveGame.Persistence.Repositories;
using DetectiveGame.Domain.Entities;
using DetectiveGame.Application.Repositories;

namespace DetectiveGame.Persistence.Repositories
{
    public class GameRepository : Repository<Game>, IGameRepository
    {
        public GameRepository(DetectiveGameDbContext context) : base(context)
        {
        }

        public async Task<Game> GetGameWithDetailsAsync(Guid id)
        {
            return await _context.Games
                .Include(x => x.Players)
                .Include(x => x.Evidences)
                .Include(x => x.Notes)
                .FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<IEnumerable<Game>> GetActiveGamesAsync()
        {
            return await _context.Games
                .Where(x => x.Status != GameStatus.Completed)
                .Include(x => x.Players)
                .ToListAsync();
        }
    }
} 