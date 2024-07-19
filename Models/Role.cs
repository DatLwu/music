using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace demo.Models{
    [Table("Role")]
    public class Role{
        [Key]
        public int RoleID { get; set; }
        public string RoleName { get; set; }
        public ICollection<User>? User { get; set; }
    }
}