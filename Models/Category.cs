using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace demo.Models{
    [Table("Category")]
    public class Category{
        [Key]
        public int CategoryID { get; set; }
        public string CategoryName { get; set; }
        public ICollection<songs>? songs { get; set; } 
    }
}