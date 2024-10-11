using System.ComponentModel.DataAnnotations;

namespace Latihan.Models
{
    public class Role
    {
        [Key]
        public string? RoleId {  get; set; }
        [Required(ErrorMessage = "Role Name is required!")]
        public string RoleName { get; set; }
    }
}
