using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CompanyAPI.Models
{
    public class Employee
    {

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string? Title { get; set; }


        [Required(ErrorMessage = "The FirstName field is required.")]
        public string FirstName { get; set; }


        [Required(ErrorMessage = "The LastName field is required.")]
        public string LastName { get; set; }

        [Phone]
        public string? Phone { get; set; }

        [EmailAddress]
        public string? Email { get; set; } 

        public Employee() { }
        public Employee(string? title, string firstName, string lastName, string? phone, string? email)
        {
            Title = title;
            FirstName = firstName;
            LastName = lastName;
            Phone = phone;
            Email = email;
        }
    }
}
