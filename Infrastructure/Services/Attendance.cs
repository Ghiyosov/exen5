
using Infrastructure.Models;
using Infrastructure.Interface;
using Npgsql;

namespace Infrastructure.Services;

public class AttendanceService : IAttendanceServices
{
    public bool CreateAttendance(Attendance attendance)
    {
        using(NpgsqlConnection connection = new NpgsqlConnection(SqlCommands.mainConnection))
        {
            connection.Open();
            NpgsqlCommand command = connection.CreateCommand();
            command.CommandText=@"insert into attendance(StudentId,CourseId,Date,Status)
            values(@StudentId,@CourseId,@Date,@Grade)";
            command.Parameters.AddWithValue("@StudentId",attendance.StudentId);
            command.Parameters.AddWithValue("@CourseId",attendance.CourseId);
            command.Parameters.AddWithValue("@Date",attendance.Date);
            command.Parameters.AddWithValue("@Status",attendance.Status);
            int res=command.ExecuteNonQuery();
            if(res!=0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }

    public List<Attendance> GetAttendances()
    {
        List<Attendance> attendances = new List<Attendance>();
        using(NpgsqlConnection connection = new NpgsqlConnection(SqlCommands.mainConnection))
        {
            connection.Open();
            NpgsqlCommand command = connection.CreateCommand();
            command.CommandText="select * from attendances;";
            using(NpgsqlDataReader reader = command.ExecuteReader())
            {
                while(reader.Read())
                {
                    Attendance attendance = new Attendance();
                    attendance.AttendanceId=reader.GetInt32(0);
                    attendance.StudentId=reader.GetInt32(1);
                    attendance.CourseId=reader.GetInt32(2);
                    attendance.Date=reader.GetDateTime(3);
                    attendance.Status=reader.GetString(4);
                    attendances.Add(attendance);
                }
            }
        }
        return attendances;
    }

    public bool DeleteAttendance(int id)
    {
        using(NpgsqlConnection connection = new NpgsqlConnection(SqlCommands.mainConnection))
        {
            connection.Open();
            NpgsqlCommand command = connection.CreateCommand();
            command.CommandText="delete attendance where id=@id;";
            command.Parameters.AddWithValue("@id",id);
            int res = command.ExecuteNonQuery();
            if(res!=0)
            {
                return true;
            }
            else
            {
            return false;
            }
        }
    }

    public bool UpdateAttendance(Attendance attendance)
    {
        using(NpgsqlConnection connection = new NpgsqlConnection(SqlCommands.mainConnection))
        {
            connection.Open();
            NpgsqlCommand command = connection.CreateCommand();
            command.CommandText=@"update attendance
             set CourseId=@CourseId,
             StudentId=@StudentId,
             Date=@Date,
             Status=@Status
             where id = @id";
             command.Parameters.AddWithValue("@CourseId",attendance.CourseId);
             command.Parameters.AddWithValue("@StudentId",attendance.StudentId);
             command.Parameters.AddWithValue("@Date",attendance.Date);
             command.Parameters.AddWithValue("@Status",attendance.Status);
             command.Parameters.AddWithValue("@id",attendance.AttendanceId);
             int res = command.ExecuteNonQuery();
             if(res!=0)
             {
                return true;
             }
             else
             {
                return true;
             }
        }
    }

}


file class SqlCommands
{
    public static string baseConnection="Server=postgres;Port=5432;Database=postgres;Username=postgres;Password=12345;";
    public static string mainConnection="Server=postgres;Port=5432;Database=softclub_db;Username=postgres;Password=12345;";
}



