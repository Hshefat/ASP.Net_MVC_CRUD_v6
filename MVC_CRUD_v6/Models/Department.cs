using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MVC_CRUD_v6.Models
{
    [Table("Department",Schema = "dbo")]
    public class Department
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Display(Name ="Department ID")]
        public int DepartmentId { get; set; }

        [Required]
        [Display(Name ="Department Name")]
        [Column(TypeName ="varchar(150)")]
        public string DepartmentName { get; set; }

        [Column(TypeName ="varchar(5)")]
        [Display(Name ="Department Abbreviation")]
        public string DepartmentAbbr { get; set; }

    }
}
