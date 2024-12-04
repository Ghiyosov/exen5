namespace Infrastructure.Models;

public class Submissions
{
    public int SubmissionId { get; set; }
    public int AssigmentId { get; set; }
    public int StudentId { get; set; }
    public DateTime SubmissionDate { get; set; }
    public decimal Grade { get; set; }

}

