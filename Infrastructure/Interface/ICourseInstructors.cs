using Infrastructure.Models;

namespace Infrastructure.Interface;

public interface ICourseInstructors
{
    List<CourseInstructors> GetCourseInstructors();
    CourseInstructors? GetCourseInstructor(int courseId);
    bool AddCourseInstructor(CourseInstructors courseInstructor);
    bool RemoveCourseInstructor(int courseId);
    bool UpdateCourseInstructor(CourseInstructors courseInstructor);
}