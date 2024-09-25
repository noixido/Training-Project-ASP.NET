using Latihan.ViewModels;

namespace Latihan.Repositories.Interface
{
    public interface IRegisterRepository
    {
        int Register(RegisterVM registerVM);
        RegisterVM lastInsertedEmpData();
        IEnumerable<ShowDataVM> GetAllEmpData();
        bool Login(LoginVM loginVM);

        IEnumerable<CountDegreeVM> GetCountDegree();
    }
}
