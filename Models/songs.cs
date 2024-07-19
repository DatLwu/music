using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace demo.Models{
    [Table("songs")]
    public class songs{
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int SongID { get; set; }
        public string Song_Name { get; set; }
        public string Artist { get; set; }
        public string File_path { get; set; }
        public int CategoryID { get; set; }
        [ForeignKey("CategoryID")]
        public Category? Category {get; set ;}
        public int ListenCount { get; set; }

    }
}