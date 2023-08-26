using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CompanyAPI.Models
{
    public class Company
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "The Code field is required.")]
        [MaxLength(10)]
        public string Code { get; set; }

        [Required(ErrorMessage = "The Name field is required.")]
        public string Name { get; set; }

        [Required(ErrorMessage = "The DirectorId field is required.")]
        public int DirectorId { get; set; }

        [ForeignKey("DirectorId")]
        public Employee Director { get; set; }
    }
}
