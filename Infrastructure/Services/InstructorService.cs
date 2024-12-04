using Infrastructure.Interface;
using Infrastructure.Models;
using Npgsql;

namespace Infrastructure.Services;

public class InstructorService:IInstructorService
{
    private const string connectionString = " Server= localhost; port=5432; user id = postgres ; database = poostgres ; password = 1234; ";

    public List<Instructors> getAllInstructors()
    {
        List<Instructors> instructors = new();
        using NpgsqlConnection connection= new NpgsqlConnection(connectionString);
        connection.Open();
        NpgsqlCommand command = connection.CreateCommand();
        command.CommandText = "SELECT * FROM Instructors";
        NpgsqlDataReader reader = command.ExecuteReader();
        if (reader.HasRows)
        {
            while (reader.Read())
            {
               Instructors instructor = new()
                {
                    InstructorId = reader.GetInt32(0),
                    FirstName = reader.GetString(1),
                    LastName = reader.GetString(2),
                    Email = reader.GetString(3),
                    Phone = reader.GetString(4),
                    HireDate = reader.GetDateTime(5)
                };
                instructors.Add(instructor);
                
            }
        }
        return instructors;
    }

    public Instructors getInstructorById(string id)
    {
        Instructors res = new();
        using NpgsqlConnection connection = new NpgsqlConnection(connectionString);
        connection.Open();
        NpgsqlCommand command = connection.CreateCommand();
        command.CommandText = "SELECT * FROM Instructors WHERE InstructorId = @InstructorId";
        command.Parameters.AddWithValue("@InstructorId", id);
        NpgsqlDataReader reader = command.ExecuteReader();
        if (reader.HasRows)
        {
            while (reader.Read())
            {
                Instructors instructor = new()
                {
                    InstructorId = reader.GetInt32(0),
                    FirstName = reader.GetString(1),
                    LastName = reader.GetString(2),
                    Email = reader.GetString(3),
                    Phone = reader.GetString(4),
                    HireDate = reader.GetDateTime(5)

                };
                res=instructor;
                
                
            }
        }
        return res;
    }

    public bool addInstructor(Instructors instructor)
    {
        NpgsqlConnection connection = new NpgsqlConnection(connectionString);
        connection.Open();
        NpgsqlCommand command = connection.CreateCommand();
        command.CommandText="insert into Instructors(InstructorId,FirstName,LastName,Email,Phone,Hiredate)values(@InstructorId,@FirstName,@LastName,@Email,@Phone,@HiredateI)";
        command.Parameters.AddWithValue("@InstructorId", instructor.InstructorId);
        command.Parameters.AddWithValue("@FirstName", instructor.FirstName);
        command.Parameters.AddWithValue("@LastName", instructor.LastName);
        command.Parameters.AddWithValue("@Email", instructor.Email);
        command.Parameters.AddWithValue("@Phone", instructor.Phone);
        command.Parameters.AddWithValue("@HiredateI", instructor.HireDate);
        int res = command.ExecuteNonQuery();
        if (res == 0) return false;
        return true;
    }

    public bool updateInstructor(Instructors instructor)
    {
        using NpgsqlConnection connection = new(connectionString);
        connection.Open();
        NpgsqlCommand command = connection.CreateCommand();
        command.CommandText = "update Instructor set InstructorId=@InstructorId, where id =@id;";
        command.Parameters.AddWithValue("@InstructorId", instructor.InstructorId);
        command.Parameters.AddWithValue("@FirstName", instructor.FirstName);
        command.Parameters.AddWithValue("@LastName", instructor.LastName);
        command.Parameters.AddWithValue("@Email", instructor.Email);
        command.Parameters.AddWithValue("@Phone", instructor.Phone);
        command.Parameters.AddWithValue("@HiredateI", instructor.HireDate);
        int res = command.ExecuteNonQuery();
        if (res == 0) return false;
        return true;
        
    }

    public bool deleteInstructor(string id)
    {
        
        using NpgsqlConnection connection = new(connectionString);
        connection.Open();
        NpgsqlCommand command = connection.CreateCommand();
        command.CommandText = "delete from Instructors where id =@id;";
        command.Parameters.AddWithValue("@id", id);
        int res = command.ExecuteNonQuery();
        if (res == 0) return false;
        return true;
    }

}