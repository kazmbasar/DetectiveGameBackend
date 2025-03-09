using System;


namespace DetectiveGame.Domain.Entities
{
    public class Evidence : BaseEntity
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public string ImageUrl { get; set; }
        public Guid GameId { get; set; }
        public virtual Game Game { get; set; }
    }
} 