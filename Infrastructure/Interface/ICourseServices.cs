using Infrastructure.Models;

namespace Infrastructure.Interface;

public interface ICourseServices
{
    bool CreateCourse(Course course);
    List<Course> GetCourses();
    Course GetCourseById(int id);
    bool Update(Course course);
    bool Delete(int id);
}