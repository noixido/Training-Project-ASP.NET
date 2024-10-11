using Latihan.Models;
using Latihan.ViewModels;

namespace Latihan.Repositories.Interface
{
    public interface IRegisterRepository
    {
        int Register(RegisterVM registerVM);
        RegisterVM lastInsertedEmpData();
        IEnumerable<ShowDataVM> GetAllEmpData();
        bool Login(LoginVM loginVM);
        ShowDataVM GetEmpByEmail(string email);
        //RegisterVM GetEmployeeByEmail(string email);
        UpdateProfileVM GetEmployeeByEmail(string email);
        int updateEmployee(UpdateProfileVM updateProfileVM);

        int changePassword(ChangePassVM changePassVM);

        IEnumerable<CountDegreeVM> GetCountDegree();
        PayloadVM GetPayload(string email);
    }
}
