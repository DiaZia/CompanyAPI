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

        public Employee CreateEmployee(Employee newEmployee)
        {
            if (IsPhoneNumberValid(newEmployee.Phone) && IsEmailValid(newEmployee.Email)) {
                dbContext.Employee.Add(newEmployee);
                dbContext.SaveChanges();
                return newEmployee;
            }
            return null;
        }

        public Employee UpdateEmployee(Employee employee)
        {
            if (IsPhoneNumberValid(employee.Phone) && IsEmailValid(employee.Email))
            {
                var employeeInDb = dbContext.Employee.Find(employee.Id);

                if (employeeInDb == null)
                {
                    throw new ArgumentException("Employee not found.", nameof(employee.Id));
                }

                employeeInDb.Title = employee.Title;
                employeeInDb.FirstName = employee.FirstName;
                employeeInDb.LastName = employee.LastName;
                employeeInDb.Phone = employee.Phone;
                employeeInDb.Email = employee.Email;

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


        public bool IsPhoneNumberValid(string phoneNumber)
        {
            string phoneNumberPattern = @"^\+?(\d[\d-. ]+)?(\([\d-. ]+\))?[\d-. ]+\d$";
            if (!Regex.IsMatch(phoneNumber, phoneNumberPattern))
            {
                throw new ArgumentException("The phone number is not valid.");
            }
            return true;
        }

        public bool IsEmailValid (string email)
        {
            string emailPattern = @"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$";
            if (!Regex.IsMatch(email, emailPattern))
            {
                throw new ArgumentException("The email is not valid.");
            }
            return true;
        }
    }
}
