using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace TokenBasedAuthenticationWebAPI.POCO.Entity
{
    public class APIContext : DbContext
    {
        public APIContext() : base("DefaultConnection") { }
        public DbSet<APIUsers> APIUsers { get; set; }
        public DbSet<Employee> Employees { get; set; }
    }
}