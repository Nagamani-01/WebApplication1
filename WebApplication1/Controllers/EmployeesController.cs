using EmployeeDataAcess;
using EmployeeServices;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace WebApplication1.Controllers
{
    public class EmployeesController : ApiController
    {
        // to load employees
        [HttpGet]
        public IEnumerable<Employee> LoadALLEmployees(string gender="All")
        {
            using (EmployeeDBEntites entites = new EmployeeDBEntites())
            {
                return entites.Employees.ToList();
            }
        }

        [HttpGet]
        public Employee GetEmployeeById(int id)
        {
            using (EmployeeDBEntites entites = new EmployeeDBEntites())
            {
                return entites.Employees.FirstOrDefault(e => e.ID == id);
            }
        }

        [HttpPost]
        public int Post(Employee employee)
        {
            using (EmployeeDBEntites context = new EmployeeDBEntites())
            {
                context.Employees.Add(employee);
                return context.SaveChanges();
            }
        }

        [HttpDelete]
        public int Delete(int id)
        {
            using (EmployeeDBEntites context = new EmployeeDBEntites())
            {
                var employee = context.Employees.FirstOrDefault(e => e.ID == id);
                if (employee != null)
                {
                    context.Employees.Remove(employee);
                }
                return context.SaveChanges();
            }
        }

        [HttpPut]
        public Employee Put(Employee employee)
        {
            var employeeFound = new Employee();
            using (EmployeeDBEntites context = new EmployeeDBEntites())
            {
                // first find the employee
                employeeFound = context.Employees.FirstOrDefault(e => e.ID == employee.ID);
                if (employeeFound != null)
                {
                    employeeFound.FirstName = employee.FirstName;
                    employeeFound.LastName = employee.LastName;
                    employeeFound.Gender = employee.Gender;
                    employeeFound.Salary = employee.Salary;
                }
                context.SaveChanges();
            }
            return employeeFound;
        }
    }
}
