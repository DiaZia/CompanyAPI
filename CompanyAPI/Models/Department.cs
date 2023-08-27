using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CompanyAPI.Models
{
    public class Department
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }


        [Required(ErrorMessage = "The Code field is required.")]
        [MaxLength(10)]
        public string Code { get; set; }

        [Required(ErrorMessage = "The Name field is required.")]
        public string Name { get; set; }


        [Required(ErrorMessage = "The LeaderId field is required.")]
        public int? LeaderId { get; set; }

        [ForeignKey("LeaderId")]
        public Employee? Leader { get; set; }

        [Required(ErrorMessage = "The ProjectId field is required.")]
        public int ProjectId { get; set; }

        [ForeignKey("ProjectId")]
        public Division Project { get; set; }
    }
}
