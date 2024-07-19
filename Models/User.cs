using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace demo.Models{
    [Table("User")]
    public class User{
        [Key]
        public int UserID { get; set; }
        public string UserName { get; set; }
        public string UserPassword { get; set; }
        public int RoleID { get; set; }
        [ForeignKey("RoleID")]
        public Role? Role { get; set; }
    }
}