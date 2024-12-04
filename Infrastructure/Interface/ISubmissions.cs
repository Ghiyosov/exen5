using Infrastructure.Models;
namespace Infrastructure.Interface;


public interface ISubmissionsServices
{
    public bool CreateSubmissions(Submissions submissions);
    public List<Submissions> GetSubmissions();
    public bool UpdateSubmissions(Submissions submissions);
    public bool DeleteSubmissions(int id);
}