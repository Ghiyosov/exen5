using Infrastructure.Interface;
using Infrastructure.Models;
using Npgsql;

namespace Infrastructure.Services;

public class CourseInstructorsService:ICourseInstructors
{
    private const string connectionString = " Server= localhost; port=5432; user id = postgres ; database = poostgres ; password = 1234; ";

    
    public List<CourseInstructors> GetCourseInstructors()
    {
        List<CourseInstructors> courseInstructors = new();
        using NpgsqlConnection connection= new NpgsqlConnection(connectionString);
        connection.Open();
        NpgsqlCommand command = connection.CreateCommand();
        command.CommandText = "SELECT * FROM CourseInstructors";
        NpgsqlDataReader reader = command.ExecuteReader();
        if (reader.HasRows)
        {
            while (reader.Read())
            {
                CourseInstructors courseInstructor= new()
                {
                   CourseInstructorId = (int)reader["CourseInstructorId"],
                   CourseId = (int)reader["CourseId"],
                   InstructorId = (int)reader["InstructorId"],
                };
                courseInstructors.Add(courseInstructor);
                
            }
        }
        return courseInstructors;
    }

    public CourseInstructors? GetCourseInstructor(int courseId)
    {
        CourseInstructors responce = new();
        using NpgsqlConnection connection= new NpgsqlConnection(connectionString);
        connection.Open();
        NpgsqlCommand command = connection.CreateCommand();
        command.CommandText = "SELECT * FROM CourseInstructors where  id =@id limit 1";
        command.Parameters.AddWithValue("@id", courseId);
        NpgsqlDataReader reader = command.ExecuteReader();
        if (reader.HasRows)
        {
            while (reader.Read())
            {
                CourseInstructors courseInstructors = new()
                {
                    CourseInstructorId = (int)reader["CourseInstructorId"],
                    CourseId = (int)reader["CourseId"],
                    InstructorId = (int)reader["InstructorId"],
                };
                responce = courseInstructors;


            }
        }

        return responce;
    }

    public bool AddCourseInstructor(CourseInstructors courseInstructor)
    {
        using NpgsqlConnection connection = new(connectionString);
        connection.Open();
        NpgsqlCommand command = connection.CreateCommand();
        command.CommandText = "insert into CourseInstructors(CourseInstructorId,CourseId,InstructorId)values(@CourseInstructorId,@CourseId,@InstructorI)";
        command.Parameters.AddWithValue("@CourseInstructorId", courseInstructor.CourseInstructorId);
        command.Parameters.AddWithValue("@CourseId", courseInstructor.CourseId);
        command.Parameters.AddWithValue("@InstructorI", courseInstructor.InstructorId);
        int res = command.ExecuteNonQuery();
        if (res == 0) return false;
        return true;
    }

    public bool RemoveCourseInstructor(int courseId)
    {
        using NpgsqlConnection connection = new(connectionString);
        connection.Open();
        NpgsqlCommand command = connection.CreateCommand();
        command.CommandText = "delete from CourseInstructor where id =@id;";

        command.Parameters.AddWithValue("@id", courseId);

        int res = command.ExecuteNonQuery();
        if (res == 0) return false;
        return true;
    }

    public bool UpdateCourseInstructor(CourseInstructors courseInstructor)
    {
        using NpgsqlConnection connection = new(connectionString);
        connection.Open();
        NpgsqlCommand command = connection.CreateCommand();
        command.CommandText = "update CourseInstructorId set CourseInstructorId=@CourseInstructorId,CourseId=@CourseId,InstructorId=@InstructorId where id =@id;";
        command.Parameters.AddWithValue("@CourseInstructorId", courseInstructor.CourseInstructorId);
        command.Parameters.AddWithValue("@CourseId", courseInstructor.CourseId);
        command.Parameters.AddWithValue("@InstructorId", courseInstructor.InstructorId);
        int res = command.ExecuteNonQuery();
        if (res == 0) return false;
        return true;
    }
}