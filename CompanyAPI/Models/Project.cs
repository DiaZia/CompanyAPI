using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CompanyAPI.Models
{
    public class Project
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

        [Required(ErrorMessage = "The DivisionId field is required.")]
        public int DivisionId { get; set; }

        [ForeignKey("DivisionId")]
        public Division Division { get; set; }
    }
}
