using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace againcrud.Models
{
    public class UsersContext : DbContext
    {
        public UsersContext() :base("UsersContext") 
        {
        
        }

        public DbSet<Student> Students { get; set; }
    }
}