using Latihan.Context;
using Latihan.Models;
using Latihan.Repositories.Interface;
using Microsoft.EntityFrameworkCore;

namespace Latihan.Repositories
{
    public class RoleRepository : IRoleRepository
    {
        private readonly MyContext _context;
        public RoleRepository(MyContext context)
        {
            _context = context;
        }

        public int addRole(Role role)
        {
            role.RoleId = GenerateRoleId();

            var theRole = _context.Roles.Add(role);
            if (theRole == null)
            {
                throw new Exception("Unable to add new role");
            }
            return _context.SaveChanges();
        }

        public int deleteRole(string roleId)
        {
            var deleteRole = _context.Roles.Find(roleId);
            if (deleteRole == null)
            {
                return 0;
            }

            _context.Roles.Remove(deleteRole);
            return _context.SaveChanges();
        }

        public string GenerateRoleId()
        {
            Role role = new Role();
            var lastRecord = _context.Roles.Max(r => r.RoleId);
            if (lastRecord == null)
            {
                return "R01";
            }
            else
            {
                var lastRecordId = int.Parse(lastRecord.Substring(1));
                lastRecordId++;
                var num = lastRecordId.ToString("D2");
                var customId = $"R{num}";

                return customId;
            }
        }

        public IEnumerable<Role> GetAllRoles()
        {
            var getRole = _context.Roles.ToList();
            if (getRole == null)
            {
                throw new Exception("Data not found");
            }
            return getRole;
        }

        public Role GetRoleById(string roleId)
        {
            return _context.Roles.Find(roleId);
        }

        public int updateRole(Role role)
        {
            _context.Entry(role).State = EntityState.Modified;
            return _context.SaveChanges();
        }
    }
}
