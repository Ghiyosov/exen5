
using Infrastructure.Models;
using Infrastructure.Interface;
using Npgsql;

namespace Infrastructure.Services;

public class SubmissionsService : ISubmissionsServices
{
    public bool CreateSubmissions(Submissions submissions)
    {
        using(NpgsqlConnection connection = new NpgsqlConnection(SqlCommands.mainConnection))
        {
            connection.Open();
            NpgsqlCommand command = connection.CreateCommand();
            command.CommandText=@"insert into Submissions(AssigmentId,StudentId,SubmissionDate,Grade)
            values(@AssigmentId,@StudentId,@SubmissionDate,@Grade)";
            command.Parameters.AddWithValue("@AssigmentId",submissions.AssigmentId);
            command.Parameters.AddWithValue("@StudentId",submissions.StudentId);
            command.Parameters.AddWithValue("@SubmissionDate",submissions.SubmissionDate);
            command.Parameters.AddWithValue("@Grade",submissions.Grade);
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

    public List<Submissions> GetSubmissions()
    {
        List<Submissions> submissions = new List<Submissions>();
        using(NpgsqlConnection connection = new NpgsqlConnection(SqlCommands.mainConnection))
        {
            connection.Open();
            NpgsqlCommand command = connection.CreateCommand();
            command.CommandText="select * from Submissions;";
            using(NpgsqlDataReader reader = command.ExecuteReader())
            {
                while(reader.Read())
                {
                    Submissions submissions1=new Submissions();
                    submissions1.SubmissionId=reader.GetInt32(0);
                    submissions1.AssigmentId=reader.GetInt32(1);
                    submissions1.StudentId=reader.GetInt32(2);
                    submissions1.SubmissionDate=reader.GetDateTime(3);
                    submissions1.Grade=reader.GetDecimal(4);
                }
            }
        }
        return submissions;
    }

    public bool DeleteSubmissions(int id)
    {
        using(NpgsqlConnection connection = new NpgsqlConnection(SqlCommands.mainConnection))
        {
            connection.Open();
            NpgsqlCommand command = connection.CreateCommand();
            command.CommandText="delete Submissions where id=@id;";
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

    public bool UpdateSubmissions(Submissions submissions)
    {
        using(NpgsqlConnection connection = new NpgsqlConnection(SqlCommands.mainConnection))
        {
            connection.Open();
            NpgsqlCommand command = connection.CreateCommand();
            command.CommandText=@"update Submissions
             set AssigmentId=@AssigmentId,
             StudentId=@StudentId,
             SubmissionDate=@SubmissionDate,
             Grade=@Grade
             where id = @id";
             command.Parameters.AddWithValue("@AssigmentId",submissions.AssigmentId);
             command.Parameters.AddWithValue("@StudentId",submissions.StudentId);
             command.Parameters.AddWithValue("@SubmissionDate",submissions.SubmissionDate);
             command.Parameters.AddWithValue("@Grade",submissions.Grade);
             command.Parameters.AddWithValue("@id",submissions.SubmissionId);
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



