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

        public int? LeaderId { get; set; }

        [Required(ErrorMessage = "The DivisionId field is required.")]
        public int DivisionId { get; set; }

        public Project() { }

        public Project(string code, string name, int? leaderId, int divisionId)
        {
            Code = code;
            Name = name;
            LeaderId = leaderId;
            DivisionId = divisionId;
        }
    }
}
