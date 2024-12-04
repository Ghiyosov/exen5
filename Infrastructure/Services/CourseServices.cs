using Infrastructure.Interface;
using Infrastructure.Models;

using Npgsql;
namespace Infrastructure.Services;

public class CourseServices:ICourseServices
{
    public bool CreateCourse(Course course)
    {
        try
        {
            using (NpgsqlConnection connection = new NpgsqlConnection(SqlCommands.mainConnection))
            {
                connection.Open();
                NpgsqlCommand command = connection.CreateCommand();
                command.CommandText = SqlCommands.insertCourse;
                command.Parameters.AddWithValue("@name",course.Name);
                command.Parameters.AddWithValue("@credits",course.Credits);
                command.Parameters.AddWithValue("@description",course.Description);
                command.Parameters.AddWithValue(" @startdate",course.StartDate);
                command.Parameters.AddWithValue("@enddate",course.EndDate);
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

    public List<Course> GetCourses()
    {
        try
        {
            List<Course> courses = new List<Course>();
            using (NpgsqlConnection connection = new NpgsqlConnection(SqlCommands.mainConnection))
            {
                connection.Open();
                NpgsqlCommand command = connection.CreateCommand();
                command.CommandText = SqlCommands.getStudents;
                using (NpgsqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Course course = new Course();
                        course.Id = reader.GetInt32(0);
                        course.Name=reader.GetString(1);
                        course.Description=reader.GetString(2);
                        course.Credits=reader.GetInt32(3);
                        course.StartDate=reader.GetDateTime(4);
                        course.EndDate=reader.GetDateTime(5);
                        courses.Add(course);
                    }
                }
            }

            return courses;
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
            throw;
        }
    }

    public Course GetCourseById(int id)
    {
        try
        {
            Course course = new Course();
            using (NpgsqlConnection connection = new NpgsqlConnection(SqlCommands.mainConnection))
            {
                connection.Open();
                NpgsqlCommand command = connection.CreateCommand();
                command.CommandText = SqlCommands.getCourseById;
                command.Parameters.AddWithValue("@Id",id);
                using (NpgsqlDataReader reader = command.ExecuteReader())
                {
                    if(reader.Read())
                    {
                        course.Id = reader.GetInt32(0);
                        course.Name=reader.GetString(1);
                        course.Description=reader.GetString(2);
                        course.Credits=reader.GetInt32(3);
                        course.StartDate=reader.GetDateTime(4);
                        course.EndDate=reader.GetDateTime(5);
                    }
                }
            }

            return course;
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
            throw;
        }
    }

    public bool Update(Course course)
    {
        try
        {
            using (NpgsqlConnection connection = new NpgsqlConnection(SqlCommands.mainConnection))
            {
                connection.Open();
                NpgsqlCommand command = connection.CreateCommand();
                command.CommandText = SqlCommands.update;
                command.Parameters.AddWithValue("@Id",course.Id);
                command.Parameters.AddWithValue("@name",course.Name);
                command.Parameters.AddWithValue("@credits",course.Credits);
                command.Parameters.AddWithValue("@description",course.Description);
                command.Parameters.AddWithValue(" @startdate",course.StartDate);
                command.Parameters.AddWithValue("@enddate",course.EndDate);
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
    public static string insertCourse = "insert into Courses( name, description,credits, startdate, enddate) values ( @name,@description, @credits, @startdate, @enddate);\n";
    public static string getStudents = "select * from courses";
    public static string getCourseById = "select * from courses where CourseId=@Id";
    public static string update = "update Courses set Credits=@Credits,Description=@Description,StartDate=@StartDate,EndDate=@EndDate where CourseId=@Id;";
    public static string delete = "delete from Courses where CourseId=@Id;";
};