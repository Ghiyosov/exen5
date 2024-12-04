using Infrastructure.Interface;
using Infrastructure.Models;
using Npgsql;

namespace Infrastructure.Services;

public class StudentServices : IStudentServices
{
    public bool CreateStudent(Student student)
    {
        try
        {
            using (NpgsqlConnection connection = new NpgsqlConnection(SqlCommands.mainConnection))
            {
                connection.Open();
                NpgsqlCommand command = connection.CreateCommand();
                command.CommandText = SqlCommands.insertStudent;
                command.Parameters.AddWithValue("@firstname",student.FirstName);
                command.Parameters.AddWithValue("@lastname",student.LastName);
                command.Parameters.AddWithValue("@email",student.Email);
                command.Parameters.AddWithValue("@phone",student.Phone);
                command.Parameters.AddWithValue("@EnrolledDate",student.EnrolledDate);
                int b = command.ExecuteNonQuery();
                if (b>0) return true;
                else return false;
            }
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
            return false;
        }
    }

    public List<Student> GetStudents()
    {
        try
        {
            List<Student> students = new List<Student>();
            using (NpgsqlConnection connection = new NpgsqlConnection(SqlCommands.mainConnection))
            {
                 connection.Open();
                 NpgsqlCommand command = connection.CreateCommand();
                 command.CommandText = SqlCommands.getStudents;
                 using (NpgsqlDataReader reader = command.ExecuteReader())
                 {
                     while (reader.Read())
                     {
                         Student student = new Student();
                         student.Id = reader.GetInt32(0);
                         student.FirstName = reader.GetString(1);
                         student.LastName = reader.GetString(2);
                         student.Email = reader.GetString(3);
                         student.Phone = reader.GetString(4);
                         student.EnrolledDate = reader.GetDateTime(5);
                         students.Add(student);
                     }
                 }
            }

            return students;
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
            throw;
        }
    }

    public Student GetStudentById(int id)
    {
        try
        { 
            Student student = new Student();
            using (NpgsqlConnection connection = new NpgsqlConnection(SqlCommands.mainConnection))
            {
                connection.Open();
                NpgsqlCommand command = connection.CreateCommand();
                command.CommandText = SqlCommands.getStudentById;
                command.Parameters.AddWithValue("@Id",id);
                using (NpgsqlDataReader reader = command.ExecuteReader())
                {
                    if(reader.Read())
                    {
                        student.Id = reader.GetInt32(0);
                        student.FirstName = reader.GetString(1);
                        student.LastName = reader.GetString(2);
                        student.Email = reader.GetString(3);
                        student.Phone = reader.GetString(4);
                        student.EnrolledDate = reader.GetDateTime(5);
                    }
                }
            }

            return student;
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
            throw;
        }
    }

    public bool Update(Student student)
    {
        try
        {
            using (NpgsqlConnection connection = new NpgsqlConnection(SqlCommands.mainConnection))
            {
                connection.Open();
                NpgsqlCommand command = connection.CreateCommand();
                command.CommandText = SqlCommands.update;
                command.Parameters.AddWithValue("@Id",student.Id);
                command.Parameters.AddWithValue("@firstname",student.FirstName);
                command.Parameters.AddWithValue("@lastname",student.LastName);
                command.Parameters.AddWithValue("@email",student.Email);
                command.Parameters.AddWithValue("@phone",student.Phone);
                command.Parameters.AddWithValue("@EnrolledDate",student.EnrolledDate);
                int b = command.ExecuteNonQuery();
                if (b > 0) return true;
                else return false;
            }
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
             return false;
            
        }
    }

    public bool Delete(int id)
    {
        try
        {
            using (NpgsqlConnection connection = new NpgsqlConnection(SqlCommands.mainConnection))
            {
                connection.Open();
                NpgsqlCommand command = connection.CreateCommand();
                command.CommandText = SqlCommands.delete;
                command.Parameters.AddWithValue("@Id",id);
                int b = command.ExecuteNonQuery();
                if (b > 0) return true;
                else return false;
            }
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
}

file class SqlCommands
{
    public static string baseConnection = "Server=localhost;Port=5432;Database=postgres;Username=postgres;Password=12345";
    public static string mainConnection = "Server=localhost;Port=5432;Database=postgres;Username=postgres;Password=12345";
    public static string insertStudent = "insert into students(firstname, lastname, email, phone, EnrolledDate) values (@firstname,@lastname,@email,@phone,@EnrolledDate);";
    public static string getStudents = "select * from students";
    public static string getStudentById = "select * from students where StudentId=@Id";
    public static string update = "update students set firstname=@firstname,lastname=@lastname,email=@email,phone=@phone,EnrolledDate=@EnrolledDate where StudentId=@Id;";
    public static string delete = "delete from students where StudentId=@Id;";
};