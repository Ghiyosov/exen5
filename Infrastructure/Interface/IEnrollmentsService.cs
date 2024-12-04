using Infrastructure.Models;

namespace Infrastructure.Interface;

public interface IEnrollmentsService
{
    List<Enrollments> GetEnrollments();
    Enrollments GetEnrollmentById(string id);
    bool AddEnrollment(Enrollments enrollment);
    bool UpdateEnrollment(Enrollments enrollment);
    bool DeleteEnrollment(string id);
}