using Infrastructure.Models;

namespace Infrastructure.Interface;

public interface IInstructorService
{
    List<Instructors> getAllInstructors();
    Instructors getInstructorById(string id);
    bool addInstructor(Instructors instructor);
    bool updateInstructor(Instructors instructor);
    bool deleteInstructor(string id);
}