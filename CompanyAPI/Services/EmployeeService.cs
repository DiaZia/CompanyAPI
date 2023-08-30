using CompanyAPI.Data;
using CompanyAPI.Models;
using System.Text.RegularExpressions;

namespace CompanyAPI.Services
{
    public class EmployeeService
    {
        private readonly ApiContext dbContext;

        public EmployeeService(ApiContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public Employee? CreateEmployee(Employee newEmployee)
        {
            if (IsPhoneNumberValid(newEmployee.Phone) && IsEmailValid(newEmployee.Email)) {
                dbContext.Employee.Add(newEmployee);
                dbContext.SaveChanges();
                return newEmployee;
            }
            return null;
        }

        public Employee UpdateEmployee(int id, string? title, string? firstName, string? lastName, string? phone, string? email)
        {
            if (IsPhoneNumberValid(phone) && IsEmailValid(email))
            {
                var employeeInDb = dbContext.Employee.Find(id);

                if (employeeInDb == null)
                {
                    throw new ArgumentException("Employee not found.");
                }

                if (title != null)
                {
                    employeeInDb.Title = title;
                }
                if (firstName != null)
                {
                    employeeInDb.FirstName = firstName;
                }
                if (lastName != null)
                {
                    employeeInDb.LastName = lastName;
                }
                if (phone != null)
                {
                    employeeInDb.Phone = phone;
                }
                if (email  != null) 
                {
                    employeeInDb.Email = email;
                }

                dbContext.SaveChanges();
                return employeeInDb;
            }
            return null;
        }

        public void DeleteEmployee(int id)
        {
            var employee = dbContext.Employee.Find(id);

            if (employee == null)
            {
                throw new ArgumentException("Employee not found.", nameof(id));
            }

            foreach (var company in dbContext.Company.Where(c => c.DirectorId == id))
            {
                company.DirectorId = null;
            }

            foreach (var division in dbContext.Division.Where(d => d.LeaderId == id))
            {
                division.LeaderId = null;
            }

            foreach (var department in dbContext.Department.Where(d => d.LeaderId == id))
            {
                department.LeaderId = null;
            }

            foreach (var project in dbContext.Project.Where(p => p.LeaderId == id))
            {
                project.LeaderId = null;
            }

            dbContext.Employee.Remove(employee);
            dbContext.SaveChanges();
        }

        public Employee GetEmployeeById(int id)
        {
            var employee = dbContext.Employee.Find(id);

            if (employee == null)
            {
                throw new ArgumentException("Employee not found.", nameof(id));
            }

            return employee;
        }


        public bool IsPhoneNumberValid(string? phoneNumber)
        {
            string phoneNumberPattern = @"^\+?(\d[\d-. ]+)?(\([\d-. ]+\))?[\d-. ]+\d$";
            if (phoneNumber != null && !Regex.IsMatch(phoneNumber, phoneNumberPattern))
            {
                throw new ArgumentException("The phone number is not valid.");
            }
            return true;
        }

        public bool IsEmailValid (string? email)
        {
            string emailPattern = @"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$";
            if (email != null && !Regex.IsMatch(email, emailPattern))
            {
                throw new ArgumentException("The email is not valid.");
            }
            return true;
        }
    }
}
