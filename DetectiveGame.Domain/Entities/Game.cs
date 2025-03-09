using System;
using System.Collections.Generic;


namespace DetectiveGame.Domain.Entities
{
    public class Game : BaseEntity
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public GameStatus Status { get; set; }
        public virtual ICollection<Player> Players { get; set; }
        public virtual ICollection<Evidence> Evidences { get; set; }
        public virtual ICollection<Note> Notes { get; set; }
    }
} 