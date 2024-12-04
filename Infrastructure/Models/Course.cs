namespace Infrastructure.Models;

public class Course
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public int  Credits { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }  
}