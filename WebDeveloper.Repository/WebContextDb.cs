using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using WebDeveloper.Model;
using System.Data.Entity.ModelConfiguration.Conventions;



namespace WebDeveloper.Repository
{
     public class WebContextDb : DbContext

    {
        public WebContextDb() : base("WebDeveloperConnectionString")
        {



        }

        public DbSet <Person> Persons { get; set;}

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
           // base.OnModelCreating(modelBuilder);

        }

    }
}
