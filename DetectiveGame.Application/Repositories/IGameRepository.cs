using DetectiveGame.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;


namespace DetectiveGame.Application.Repositories
{
    public interface IGameRepository : IRepository<Game>
    {
        Task<Game> GetGameWithDetailsAsync(Guid id);
        Task<IEnumerable<Game>> GetActiveGamesAsync();
    }
} 