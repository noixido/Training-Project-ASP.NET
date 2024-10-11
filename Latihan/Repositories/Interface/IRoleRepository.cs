using Latihan.Models;

namespace Latihan.Repositories.Interface
{
    public interface IRoleRepository
    {
        IEnumerable<Role> GetAllRoles();
        Role GetRoleById(string roleId);
        int addRole(Role role);
        int updateRole(Role role);
        int deleteRole(string roleId);

        string GenerateRoleId();
    }
}
