using careersfair.Models;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace careersfair.DAL
{

    /// <summary>
    /// FormContext class, this class is of DbContext and implements the use of entity framework. 
    /// Entity framework handles all of the database work.
    /// Created: by: Gavan
    /// Version: 1.0
    /// </summary>
    public class FormContext : DbContext
    {
        public DbSet<Form> form;

        public FormContext() : base("FormDBContext") { }

        /// <summary>
        /// The form context of base, base contains the name of the connection string
        /// </summary>
        public DbSet<Form> Form { get; set; }
        public DbSet<FormResults> FormResults { get; set; }

        /// <summary>
        /// The DbSet array for form model, the items that entity framework must deal with
        /// </summary>
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        }
    }
}