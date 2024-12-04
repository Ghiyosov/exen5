using Infrastructure.Models;
namespace Infrastructure.Interface;

public interface IAttendanceServices
{
    public bool CreateAttendance(Attendance attendance);
    public List<Attendance> GetAttendances();
    public bool UpdateAttendance(Attendance attendance);
    public bool DeleteAttendance(int id);
}
