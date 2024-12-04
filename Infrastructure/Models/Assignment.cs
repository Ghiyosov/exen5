namespace Infrastructure.Models;

public class Assignment
{
    public int Id { get; set; }
    public int  CurseId { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public DateTime DueDate { get; set; }
}