using CompanyAPI.Data;
using CompanyAPI.Models;

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
            dbContext.Employees.Add(newEmployee);
            dbContext.SaveChanges();
            return newEmployee;
        }

        public Employee UpdateEmployee(Employee employee)
        {
            var employeeInDb = dbContext.Employees.Find(employee.Id);

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

        public void DeleteEmployee(int id)
        {
            var employee = dbContext.Employees.Find(id);

            if (employee == null)
            {
                throw new ArgumentException("Employee not found.", nameof(id));
            }

            dbContext.Employees.Remove(employee);
            dbContext.SaveChanges();
        }

        public Employee GetEmployeeById(int id)
        {
            var employee = dbContext.Employees.Find(id);

            if (employee == null)
            {
                throw new ArgumentException("Employee not found.", nameof(id));
            }

            return employee;
        }
    }
}
