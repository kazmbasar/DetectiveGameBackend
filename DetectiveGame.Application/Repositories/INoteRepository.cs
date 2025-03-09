using DetectiveGame.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;


namespace DetectiveGame.Application.Repositories
{
    public interface INoteRepository : IRepository<Note>
    {
        Task<IEnumerable<Note>> GetNotesByGameIdAsync(Guid gameId);
        Task<IEnumerable<Note>> GetNotesByPlayerIdAsync(Guid playerId);
    }
} 