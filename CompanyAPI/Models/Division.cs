using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.IO;

namespace CompanyAPI.Models
{
    public class Division
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

        [Required(ErrorMessage = "The CompanyId field is required.")]
        public int CompanyId { get; set; }

        public Division() { }
        public Division(string code, string name, int? leaderId, int company)
        {
            Code = code;
            Name = name;
            LeaderId = leaderId;
            CompanyId = company;
        }
    }
}
