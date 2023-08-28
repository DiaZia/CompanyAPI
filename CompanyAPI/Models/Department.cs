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

        public int? LeaderId { get; set; }

        [Required(ErrorMessage = "The ProjectId field is required.")]
        public int ProjectId { get; set; }

        public Department() { }

        public Department (string code, string name, int? leaderId, int projectId)
        {
            Code = code;
            Name = name;
            LeaderId = leaderId;
            ProjectId = projectId;
        }
    }
}
