using careersfair.Models;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace careersfair.DAL{
    public class FormContext : DbContext {
        public FormContext() : base("FormDBContext"){
        }

        public DbSet<Form> Form { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder){
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        }
    }
}