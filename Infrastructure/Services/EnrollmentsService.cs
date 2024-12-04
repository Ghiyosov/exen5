using Infrastructure.Interface;
using Infrastructure.Models;
using Npgsql;

namespace Infrastructure.Services;

public class EnrollmentsService:IEnrollmentsService
{
    
    private const string connectionString = " Server= localhost; port=5432; user id = postgres ; database = poostgres ; password = 1234; ";


    public List<Enrollments> GetEnrollments()
    {
        List<Enrollments> enrollments = new();
        using NpgsqlConnection connection= new NpgsqlConnection(connectionString);
        connection.Open();
        NpgsqlCommand command = connection.CreateCommand();
        command.CommandText = "SELECT * FROM Enrollments";
        NpgsqlDataReader reader = command.ExecuteReader();
        if (reader.HasRows)
        {
            while (reader.Read())
            {
                Enrollments enrollment = new()
                {
                    EnrollmentId = reader.GetInt32(0),
                    StudenttId = reader.GetInt32(1),
                    CourseId = reader.GetInt32(2),
                    Status = reader.GetString(3),
                    EnrollmentDate = reader.GetDateTime(4)
                };
                enrollments.Add(enrollment);
                
            }
        }
        return enrollments;
    }

    public Enrollments GetEnrollmentById(string id)
    {
        Enrollments responce = new();
        using NpgsqlConnection connection= new NpgsqlConnection(connectionString);
        connection.Open();
        NpgsqlCommand command = connection.CreateCommand();
        command.CommandText = "SELECT * FROM Enrollments where  id =@id limit 1";
        command.Parameters.AddWithValue("@id", id);
        NpgsqlDataReader reader = command.ExecuteReader();
        if (reader.HasRows)
        {
            while (reader.Read())
            {
                Enrollments enrollment = new()
                {
                    EnrollmentId = reader.GetInt32(0),
                    StudenttId = reader.GetInt32(1),
                    CourseId = reader.GetInt32(2),
                    Status = reader.GetString(3),
                    EnrollmentDate = reader.GetDateTime(4)
                };
                responce = enrollment;


            }
        }

        return responce;
    }

    public bool AddEnrollment(Enrollments enrollment)
    {
        
        using NpgsqlConnection connection = new(connectionString);
        connection.Open();
        NpgsqlCommand command = connection.CreateCommand();
        command.CommandText = "insert into Enrolments(StudentId,CourseId,Status,EnrolmentDate)values(@StudentId,@CourseId,@Status,@EnrolmentDate)";
        command.Parameters.AddWithValue("@StudentId", enrollment.StudenttId);
        command.Parameters.AddWithValue("@CourseId", enrollment.CourseId);
        command.Parameters.AddWithValue("@Status", enrollment.Status);
        command.Parameters.AddWithValue("@EnrolmentDate", enrollment.EnrollmentDate);
        int res = command.ExecuteNonQuery();
        if (res == 0) return false;
        return true;
    }

    public bool UpdateEnrollment(Enrollments enrollment)
    {
        using NpgsqlConnection connection = new(connectionString);
        connection.Open();
        NpgsqlCommand command = connection.CreateCommand();
        command.CommandText = "update enrollments set StudentId=@StudentId,CourseId=@CourseId,Status=@Status, EnrolmentDate=@EnrolmentDate where id =@id;";
        command.Parameters.AddWithValue("@StudentId", enrollment.StudenttId);
        command.Parameters.AddWithValue("@CourseId", enrollment.CourseId);
        command.Parameters.AddWithValue("@Status", enrollment.Status);
        command.Parameters.AddWithValue("@EnrolmentDate", enrollment.EnrollmentDate);
        int res = command.ExecuteNonQuery();
        if (res == 0) return false;
        return true;
    }

    public bool DeleteEnrollment(string id)
    {
        using NpgsqlConnection connection = new(connectionString);
        connection.Open();
        NpgsqlCommand command = connection.CreateCommand();
        command.CommandText = "delete from Enrollments where id =@id;";

        command.Parameters.AddWithValue("id", id);

        int res = command.ExecuteNonQuery();
        if (res == 0) return false;
        return true;
    }

}