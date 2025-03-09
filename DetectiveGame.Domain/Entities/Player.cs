using System;
using System.Collections.Generic;


namespace DetectiveGame.Domain.Entities
{
    public class Player : BaseEntity
    {
        public string Username { get; set; }
        public string ConnectionId { get; set; }
        public Guid GameId { get; set; }
        public virtual Game Game { get; set; }
        public virtual ICollection<Note> Notes { get; set; }
    }
} 