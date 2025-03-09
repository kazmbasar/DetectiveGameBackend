using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DetectiveGame.Application.Repositories;
using DetectiveGame.Domain.Entities;
using DetectiveGame.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;


namespace DetectiveGame.Persistence.Repositories
{
    public class NoteRepository : Repository<Note>, INoteRepository
    {
        public NoteRepository(DetectiveGameDbContext context) : base(context)
        {
        }

        public async Task<IEnumerable<Note>> GetNotesByGameIdAsync(Guid gameId)
        {
            return await _context.Notes
                .Where(n => n.GameId == gameId)
                .Include(n => n.Player)
                .OrderByDescending(n => n.CreatedDate)
                .ToListAsync();
        }

        public async Task<IEnumerable<Note>> GetNotesByPlayerIdAsync(Guid playerId)
        {
            return await _context.Notes
                .Where(n => n.PlayerId == playerId)
                .Include(n => n.Player)
                .OrderByDescending(n => n.CreatedDate)
                .ToListAsync();
        }
    }
} 