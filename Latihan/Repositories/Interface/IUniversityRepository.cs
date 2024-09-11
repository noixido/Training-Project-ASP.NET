using Latihan.Models;

namespace Latihan.Repositories.Interface
{
    public interface IUniversityRepository
    {
        IEnumerable<University> GetAllUniversities();
        University GetUniversityById(string univId);
        int addUniversity(University university);
        int updateUniversity(University university);
        int deleteUniversity(string univId);

        University GetLastInsertedData();
    }
}
