using System;


namespace DetectiveGame.Domain.Entities
{
    public class Note : BaseEntity
    {
        public string Content { get; set; }
        public Guid GameId { get; set; }
        public Guid PlayerId { get; set; }
        public virtual Game Game { get; set; }
        public virtual Player Player { get; set; }
    }
} 