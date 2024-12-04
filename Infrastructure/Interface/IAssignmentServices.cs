using Infrastructure.Models;

namespace Infrastructure.Interface;

public interface IAssignmentServices
{
    bool CreateAssignment(Assignment assignment);
    List<Assignment> GetAssignments();
    Assignment GetAssignmentById(int id);
    bool Update(Assignment assignment);
    bool Delete(int id);
}