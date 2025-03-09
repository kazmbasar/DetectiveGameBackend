using DetectiveGame.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;


namespace DetectiveGame.Application.Repositories
{
    public interface IPlayerRepository : IRepository<Player>
    {
        Task<Player> GetByConnectionIdAsync(string connectionId);
        Task<IEnumerable<Player>> GetPlayersByGameIdAsync(Guid gameId);
    }
} 