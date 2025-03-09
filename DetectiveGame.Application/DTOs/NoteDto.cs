public class NoteDto
{
    public Guid Id { get; set; }
    public string Content { get; set; }
    public Guid PlayerId { get; set; }
    public string PlayerUsername { get; set; }
    public DateTime CreatedDate { get; set; }
} 