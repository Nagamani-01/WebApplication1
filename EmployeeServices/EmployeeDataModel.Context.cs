namespace EmployeeDataAcess
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    using EmployeeServices;

    public partial class EmployeeDBEntites : DbContext
    {
        public EmployeeDBEntites() : base("name = NagamaniDBEntities")
        {

        }
        public virtual DbSet<Employee> Employees { get; set; }
    }
}