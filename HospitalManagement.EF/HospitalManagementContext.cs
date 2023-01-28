using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Validation;
using HospitalManagement.Infrastructure;
using HospitalManagement.Entities;
using System.ComponentModel.DataAnnotations;
using HospitalManagement.Repositories.EF.Configuration;
using System.Reflection;
using HospitalManagement.EF.Configuration;
using System.Data.Entity.Core.Objects;

namespace HospitalManagement.EF
{
    /// <summary>
    /// This is the main DbContext to work with data in the database.
    /// </summary>
    public class HospitalManagementContext : DbContext
    {
        /// <summary>
        /// Initializes a new instance of the DbGastenboekContext class.
        /// </summary>
        public HospitalManagementContext()
          : base("DbHospitalManagementContext1")
        {
            Configuration.LazyLoadingEnabled = false;
        }

        /// <summary>
        /// Provides access to the collection of Patient in the system.
        /// </summary>
        public DbSet<Patient> Patients { get; set; }
        public DbSet<Staff> Staffs { get; set; }

        /// <summary>
        /// Hooks into the Save process to get a last-minute chance to look at the entities and change them. Also intercepts exceptions and 
        /// wraps them in a new Exception type.
        /// </summary>
        /// <returns>The number of affected rows.</returns>
        public override int SaveChanges()
        {
            // Need to manually delete all "owned objects" that have been removed from their owner, otherwise they'll be orphaned.         
            // Using Attribut to track properties

            //var rrrs = ChangeTracker.Entries().Where(
            //    e => (e.State == EntityState.Modified || e.State == EntityState.Added) &&
            //     e.Entity.GetType().GetProperties()
            //    .Select(pi => pi.GetValue(e.Entity))
            //    .Any(value => value == null));

            //     var orphanedObjects = ChangeTracker.Entries().Where(
            //e => (e.State == EntityState.Modified || e.State == EntityState.Added) &&
            //e.Entity.GetType().GetProperties().Any(prop => Attribute.IsDefined(prop, typeof(OwnerAttribute))) &&
            //e.Entity.GetType().GetCustomAttributes<OwnerAttribute>(true)
            //         .Select(pi => pi.Link == "Key")
            //          .Any(value => value =false));

            var orphanedObjects = ChangeTracker.Entries().Where(
       e => (e.State == EntityState.Modified || e.State == EntityState.Added)) ;




            //.Select(prop => (DbEntityEntry)prop.Entity)
            //.Where(vl => (DbPropertyEntry)vl. String.IsNullOrEmpty("null"));
            // e.Entity.GetType().GetProperties().Any(prp => Attribute.IsDefined(prop.GetValue(null), typeof(OwnerAttribute));
            //e.Reference("Owner_Patient").CurrentValue == null))
            //        ;




            foreach (var orphanedObject in orphanedObjects)
            {                
               OwnerValidator.ValidateEntity(this, orphanedObject, ObjectContext.GetObjectType(orphanedObject.Entity.GetType()));

            }

            




            try
            {
                var modified = ChangeTracker.Entries().Where(e => e.State == EntityState.Modified || e.State == EntityState.Added);
                foreach (DbEntityEntry item in modified)
                {
                    var changedOrAddedItem = item.Entity as IDateTracking;
                    if (changedOrAddedItem != null)
                    {
                        if (item.State == EntityState.Added)
                        {
                            changedOrAddedItem.DateCreated = DateTime.Now;
                        }
                        changedOrAddedItem.DateModified = DateTime.Now;
                    }
                }
                return base.SaveChanges();
            }
            catch (DbEntityValidationException entityException)
            {
                var errors = entityException.EntityValidationErrors;
                var result = new StringBuilder();
                var allErrors = new List<ValidationResult>();
                foreach (var error in errors)
                {
                    foreach (var validationError in error.ValidationErrors)
                    {
                        result.AppendFormat("\r\n  Entity of type {0} has validation error \"{1}\" for property {2}.\r\n", error.Entry.Entity.GetType().ToString(), validationError.ErrorMessage, validationError.PropertyName);
                        var domainEntity = error.Entry.Entity as DomainEntity<int>;
                        if (domainEntity != null)
                        {
                            result.Append(domainEntity.IsTransient() ? "  This entity was added in this session.\r\n" : string.Format("  The Id of the entity is {0}.\r\n", domainEntity.Id));
                        }
                        allErrors.Add(new ValidationResult(validationError.ErrorMessage, new[] { validationError.PropertyName }));
                    }
                }
                throw new ModelValidationException(result.ToString(), entityException, allErrors);
            }
        }

        /// <summary>
        /// Configures the EF context.
        /// </summary>
        /// <param name="modelBuilder">The model builder that needs to be configured.</param>
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new PatientConfiguration());
            modelBuilder.Configurations.Add(new StaffConfiguration());
            modelBuilder.Configurations.Add(new DrugConfiguration());
            modelBuilder.Configurations.Add(new WardConfiguration());
            modelBuilder.Configurations.Add(new BedConfiguration());
            modelBuilder.Configurations.Add(new Patient_Drug_TreatmentConfiguration());
        }
    }
}