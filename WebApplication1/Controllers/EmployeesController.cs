using EmployeeDataAcess;
using EmployeeServices;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace WebApplication1.Controllers
{
    public class EmployeesController : ApiController
    {
        // to load employees
        [HttpGet]
        // public IEnumerable<Employee> LoadALLEmployees(string gender="All")
        public HttpResponseMessage LoadALLEmployees(string gender = "All")
        {
            using (EmployeeDBEntites entites = new EmployeeDBEntites())
            {
                switch (gender.ToLower())
                {
                    case "all":
                        return Request.CreateResponse(HttpStatusCode.OK, entites.Employees.ToList());
                    case "male":
                        return Request.CreateResponse(HttpStatusCode.OK, entites.Employees.Where(e => e.Gender.ToLower() == "male").ToList());
                    case "female":
                        return Request.CreateResponse(HttpStatusCode.OK, entites.Employees.Where(e => e.Gender.ToLower() == "Female").ToList());

                    default:
                        return Request.CreateResponse(HttpStatusCode.BadRequest, "Value for gender must be male, female or ALL." + gender + "is invalid.");
                }

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
        public int DeleteEmployeeById(int id)
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
        public Employee Put([FromBody]int id,[FromUri]Employee employee)
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
