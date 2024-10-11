using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Latihan.Models
{
    public class AccountRole
    {
        [Key]
        public int Id { get; set; }
        [ForeignKey(nameof(Role))]
        public string? RoleId { get; set; }
        [ForeignKey(nameof(Account))]
        public string NIK { get; set; }


        public virtual Role? Role { get; set; }
        public virtual Account Account { get; set; }
    }
}
