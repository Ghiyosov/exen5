namespace Infrastructure.Models;

public class Enrollments
{
    public int EnrollmentId { get; set; }
    public int StudenttId { get; set; }
    public int CourseId { get; set; }
    public string Status { get; set; } 
    
    public DateTime EnrollmentDate { get; set; }
}