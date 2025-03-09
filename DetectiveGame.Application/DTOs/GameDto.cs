public class GameDto
{
    public Guid Id { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public GameStatus Status { get; set; }
    public DateTime CreatedDate { get; set; }
    public ICollection<PlayerDto> Players { get; set; }
    public ICollection<EvidenceDto> Evidences { get; set; }
} 